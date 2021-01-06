<template>
  <div class="create">
    <h1>Opret Arbejdsopgave</h1>
    <b-alert v-if="created" variant="success" show>Din arbejdsopgave er blevet oprettet! Gå til <a href ="/">Home</a></b-alert>
    <b-alert v-if="error" variant="danger" show>Der skete en fejl.</b-alert>
    <b-alert v-if="tommefelter" variant="danger" show>Fyld felterne ud</b-alert>
    <div class="flex">
        <div>
            <p>Titel</p>
            <b-form-input v-model="title"></b-form-input>
        </div>
        <div>
            <p>Pris i DKK</p>
            <b-form-input v-model="price" type="number"></b-form-input>
        </div>
    </div>
    <div class="flex">
        <div>
            <p>Kategori</p>
            <b-form-select v-model="categorySelected">
                <b-select-option v-for="(category, index) in categories" :key="index" :value="category.id">{{ category.name }}</b-select-option>
            </b-form-select>
        </div>
        <div>
            <p>Region</p>
            <b-form-select :options="regionsField" v-model="region"></b-form-select>
        </div>
    </div>
    <p class="mt-3">Promover</p>
    <b-form-select id="input-live-promoted" v-model="promoted" :options="optionsPromoted"></b-form-select>
    <template v-if="promoted">
        <p class="mt-3">Hvornår skal promoveringen slutte?</p>
        <b-form-datepicker v-model="promotedEnd" :min="min" :max="max" locale="en"></b-form-datepicker>
        <span>Din promovering koster <span style="color: #007bff; font-weight: bold">{{ promoteDays * 25 }}</span> kr og varer {{ promoteDays }} dag<template v-if="promoteDays > 1">e</template>.</span>
    </template>
    <p class="mt-3">Beskrivelse</p>
    <b-textarea v-model="description" rows="5"></b-textarea>

    <div class="d-flex mt-4">
        <b-button @click="OpretTask()" class="ml-auto" variant="primary">Opret</b-button>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { CreateTask } from "../api/task";
import { GetCategories } from "../api/category";
import { Category } from "../types/category";
import { GetLoggedInId } from "../api/user";
import moment from "moment";

@Component
export default class Home extends Vue {
    regionsField =
        [
            { value: "Sjælland", text: "Sjælland" },
            { value: "Nordjylland", text: "Jylland" },
            { value: "Hovedstaden", text: "Hovedstaden" },
            { value: "Midtjylland", text: "Midtjylland" },
            { value: "Syddanmark", text: "Syddanmark" }
        ]

    optionsPromoted = [{ value: true, text: "Promovereret" }, { value: false, text: "Ikke promovereret" }];
    title = "";
    categorySelected = 0;
    categories = Array<Category>();
    price = 0;
    description = "";
    promoted = false;
    region = "Hovedstaden";
    dateCreated = moment().format("YYYY-MM-DD");
    promotedEnd = moment().add(1, "days").format("YYYY-MM-DD");
    max = moment().add(14, "days").format("YYYY-MM-DD");
    min = moment().add(1, "days").format("YYYY-MM-DD");
    created = false;
    error = false;
    tommefelter = false;
    promoteDays = 0;

    @Watch("categorySelected")
    OnCategoryChanged() {
        console.log(this.categorySelected);
    }

    @Watch("promotedEnd", { immediate: true }) e() {
        this.promoteDays = moment(this.promotedEnd).diff(this.dateCreated, "days");
    }

    mounted() {
        GetCategories().then(response => {
            this.categories = response.data;
            this.categorySelected = response.data[0].id;
        }).catch(() => {
            console.log("Error");
        });
    }

    OpretTask() {
        if (this.title === "" || this.price < 0 || this.description === "" || this.region === "") {
            this.tommefelter = true;
            this.error = false;
            this.created = false;
        } else {
            CreateTask(GetLoggedInId(), this.categorySelected, this.dateCreated, this.title, this.price, this.description, this.promoted, this.region, this.promotedEnd).then(() => {
                this.created = true;
                this.error = false;
                this.tommefelter = false;
                this.title = "";
                this.price = 0;
                this.description = "";
            }).catch(() => {
                this.error = true;
                this.created = false;
                this.tommefelter = false;
            });
        }
    }
}
</script>

<style scoped lang="scss">
@import "../assets/main.scss";
.flex {
    display: flex;

    div {
        width: 50%;

        &:first-of-type {
            margin-right: 5%;
        }
    }
}
.create {
    width: 700px;
    max-width: 92%;
    margin: 0 auto;
    background: $light;
    padding:40px;
    border-radius: 12px;
}
p {
    margin-bottom: 0px;
    font-weight: bold;
}
h1 {
    margin-bottom: 35px;
    font-weight: bold;
}
input[type="text"], #birthday__outer_ {
    margin-bottom: 20px;
}
</style>
