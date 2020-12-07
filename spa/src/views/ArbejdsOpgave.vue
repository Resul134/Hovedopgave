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
            <b-form-select :options="optionsField" v-model="region"></b-form-select>
        </div>
    </div>
            <p class="mt-3">Promover</p>
            <b-form-select id="input-live-promoted" v-model="promoted" :options="optionsPromoted"></b-form-select>
        <p class="mt-3">Beskrivelse</p>
        <b-textarea v-model="description" rows="5"></b-textarea>
    <!-- <b-alert v-if="promoted" variant="success" show style="padding-top: 5px;"> Din opgave er promovereret: {{ promoverPris }}</b-alert> -->
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
import moment from "moment";
import { GetBrugerById, GetLoggedInId } from "../api/user";

@Component
export default class Home extends Vue {
    optionsField = ["Region Sjælland", "Region Hovedstaden", "Region Syddanmark", "Region Midtjylland", "Region Nordjylland"];
    optionsPromoted = [{ value: true, text: "Promovereret" }, { value: false, text: "Ikke promovereret" }];
    title = "";
    categorySelected = 0;
    categories = Array<Category>(); 
    price = 0;
    description = "";
    promoted = false;
    region = "Region Hovedstaden";
    dateCreated = moment();
    promotedEnd = moment().add(7, "d");
    promoverPris = 0;
    created = false;
    error = false;
    tommefelter = false;

    @Watch("categorySelected")
    OnCategoryChanged() {
        console.log(this.categorySelected);
    }

    mounted() {
        GetCategories().then(response => {
            this.categories = response.data;
            this.categorySelected = response.data[0].id;
            console.log(response.data);
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
            CreateTask(GetLoggedInId(), this.categorySelected, this.dateCreated.toDate(), this.title, this.price, this.description, this.promoted, this.region, this.promotedEnd.toDate()).then(response => {
                console.log(response.status);
                this.created = true;
                this.error = false;
                this.tommefelter = false;
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
