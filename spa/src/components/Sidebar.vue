<template>
<div class="sidebar">
    <div>
        <h3>Kategorier</h3>
        <b-list-group class="group">
            <b-list-group-item class="item hvr-sweep-to-right" v-for="(category, idx) in categories" :key="idx" @click="filterCategory(idx)" href="" v-bind:active="category.active">
                <svg width="1.5em" height="1.5em" viewBox="0 0 16 18" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                    <path fill-rule="evenodd" v-bind:d="category.svg"/>
                </svg>
                {{ category.name }}
            </b-list-group-item>
        </b-list-group>
        <h3 class="region">Region</h3>
        <b-list-group class="group">
            <b-list-group-item class="item hvr-sweep-to-right" v-for="(region, idx) in regions" :key="idx" @click="filterRegion(region.id)" href="" v-bind:active="region.active">
                {{ region.name }}
            </b-list-group-item>
        </b-list-group>
        <h3>Pris</h3>
        <label class="from">Fra</label>
        <label class="to">Til</label>
        <div class="price">
            <b-form-input class="price" v-model="minPrice" placeholder="Pris"></b-form-input>
            <b-form-input class="price" v-model="maxPrice" placeholder="Pris"></b-form-input>
        </div>
        <h3>Dato</h3>
        <div class="date">
            <label>Fra</label>
            <b-form-input type="date" v-model="dateStart"></b-form-input>
            <label>Til</label>
            <b-form-input type="date" v-model="dateEnd"></b-form-input>
            <br>
        </div>
    </div>
</div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { Region } from "../types/region";
import { Category } from "../types/category";
import { Dictionary } from "../types/dict";
import { GetCategories } from "../api/category";

@Component
export default class Sidebar extends Vue {
    categories = [] as Category[];
    currentCategory = {} as Category;

    regions = [
        { id: 0, name: "Sjælland", active: false },
        { id: 1, name: "Nordjylland", active: false },
        { id: 2, name: "Syddanmark", active: false },
        { id: 3, name: "Hovedstaden", active: false },
        { id: 4, name: "Midtjylland", active: false }]

    currentRegion = {} as Region;
    dateStart = "2010-10-05";
    dateEnd = `${new Date().getFullYear()}-${String(new Date().getMonth() + 1).padStart(2, "0")}-${String(new Date().getDate()).padStart(2, "0")}`;
    minPrice = 0;
    maxPrice = 10000;
    queries = {} as Dictionary<string>;

    mounted() {
        GetCategories().then(response => {
            const { data } = response;
            for (let index = 0; index < data.length; index++) {
                const element: Category = data[index];
                element.active = false;
            }
            data.unshift({ id: 0, name: "Alle", active: false, svg: "" });
            this.categories = data;
        });
    }

    @Watch("minPrice") e() {
        this.NewQuery("minPrice", this.minPrice.toString());
        this.NewQuery("maxPrice", this.maxPrice.toString());
        this.ReplaceRoute(this.queries);
    }

    @Watch("maxPrice") f() {
        this.NewQuery("minPrice", this.minPrice.toString());
        this.NewQuery("maxPrice", this.maxPrice.toString());
        this.ReplaceRoute(this.queries);
    }

    @Watch("dateStart") g() {
        this.NewQuery("minDate", this.dateStart);
        this.NewQuery("maxDate", this.dateEnd);
        this.ReplaceRoute(this.queries);
    }

    @Watch("dateEnd") h() {
        this.NewQuery("minDate", this.dateStart);
        this.NewQuery("maxDate", this.dateEnd);
        this.ReplaceRoute(this.queries);
    }

    NewQuery(name: string, value: string) {
        this.queries[name] = value;
        this.$store.commit("queriesState", this.queries);
    }

    filterCategory(id: number) {
        if (this.currentCategory) {
            this.currentCategory.active = false;
        }
        if (this.currentCategory.id !== this.categories[id].id) {
            this.categories[id].active = true;
            this.currentCategory = this.categories[id];
            this.NewQuery("categoryId", this.currentCategory.id.toString());
        } else {
            this.currentCategory = {} as Category;
            this.DeleteFromQueryList("categoryId");
        }
        this.ReplaceRoute(this.queries);
    }

    filterRegion(id: number) {
        if (this.currentRegion) {
            this.currentRegion.active = false;
        }
        if (this.currentRegion.id !== this.regions[id].id) {
            this.regions[id].active = true;
            this.currentRegion = this.regions[id];
            this.NewQuery("region", this.currentRegion.name);
        } else {
            this.currentRegion = {} as Region;
            this.DeleteFromQueryList("region");
        }
        this.ReplaceRoute(this.queries);
    }

    DeleteFromQueryList(name: string) {
        delete this.queries[name];
    }

    ReplaceRoute(parameters: Dictionary<string>) {
        if (this.$route.name === "Overview") {
            this.$router.replace({ query: parameters });
        } else {
            this.$router.replace({ name: "Overview", query: parameters });
        }
    }
}
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";

h3 {
    margin: 10px 0 10px 10px;
}
.price {
    margin: 0 2%;
    display: flex;
    flex-wrap: wrap;
    input {
        width: 45%;
    }
}
.date {
    margin: 0 5%;
}
.to {
    margin: 0 30%;
}
.from {
    margin: 0 5%;
}

.sidebar {
    background: white;
    position: fixed;
    width: $sidebar-width;
    height: calc(100% - 65px);
    box-shadow: 5px 0px 5px whitesmoke;
    top: 65px;
    overflow: auto;
}

.group {
    margin: 0;
    padding: 0;
}

.item {
    border-radius: 0;
    border-right: 0;
    width: 100%;
}
.hvr-sweep-to-right {
    cursor: pointer;
    display: inline-block;
    vertical-align: middle;
    -webkit-transform: perspective(1px) translateZ(0);
    transform: perspective(1px) translateZ(0);
    box-shadow: 0 0 1px rgba(0, 0, 0, 0);
    position: relative;
    -webkit-transition-property: color;
    transition-property: color;
    -webkit-transition-duration: 0.2s;
    transition-duration: 0.2s;
}
.hvr-sweep-to-right:before {
  content: "";
  position: absolute;
  z-index: -1;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: $primary;
  -webkit-transform: scaleX(0);
  transform: scaleX(0);
  -webkit-transform-origin: 0 50%;
  transform-origin: 0 50%;
  -webkit-transition-property: transform;
  transition-property: transform;
  -webkit-transition-duration: 0.2s;
  transition-duration: 0.2s;
  -webkit-transition-timing-function: ease-out;
  transition-timing-function: ease-out;
}
.hvr-sweep-to-right:hover, .hvr-sweep-to-right:focus, .hvr-sweep-to-right:active {
  color: white;
}
.hvr-sweep-to-right:hover:before, .hvr-sweep-to-right:focus:before, .hvr-sweep-to-right:active:before {
  -webkit-transform: scaleX(1);
  transform: scaleX(1);
}
</style>
