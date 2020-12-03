<template>
<div class="sidebar">
    <div>
        <h3>Kategorier</h3>
        <b-list-group class="group">
            <b-list-group-item class="item hvr-sweep-to-right" v-for="(category, idx) in categories" :key="idx" @click="filterCategory(category.id)" href="" v-bind:active="category.active">
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
import { Query } from "../types/query";

@Component
export default class Sidebar extends Vue {
    categories = [
        { id: 0, name: "Alle", active: false, svg: "" },
        { id: 1, name: "Udendørs", active: false, svg: "M8 0a.5.5 0 0 1 .416.223l3 4.5A.5.5 0 0 1 11 5.5h-.098l2.022 3.235a.5.5 0 0 1-.424.765h-.191l1.638 3.276a.5.5 0 0 1-.447.724h-11a.5.5 0 0 1-.447-.724L3.69 9.5H3.5a.5.5 0 0 1-.424-.765L5.098 5.5H5a.5.5 0 0 1-.416-.777l3-4.5A.5.5 0 0 1 8 0zM5.934 4.5H6a.5.5 0 0 1 .424.765L4.402 8.5H4.5a.5.5 0 0 1 .447.724L3.31 12.5h9.382l-1.638-3.276A.5.5 0 0 1 11.5 8.5h.098L9.576 5.265A.5.5 0 0 1 10 4.5h.066L8 1.401 5.934 4.5z M7 13.5h2V16H7v-2.5z" },
        { id: 2, name: "IT", active: false, svg: "M13.5 3h-11a.5.5 0 0 0-.5.5V11h12V3.5a.5.5 0 0 0-.5-.5zm-11-1A1.5 1.5 0 0 0 1 3.5V12h14V3.5A1.5 1.5 0 0 0 13.5 2h-11z M0 12h16v.5a1.5 1.5 0 0 1-1.5 1.5h-13A1.5 1.5 0 0 1 0 12.5V12z" },
        { id: 3, name: "Korrektur", active: false, svg: "M8.217 11.068c1.216 0 1.948-.869 1.948-2.31v-.702c0-1.44-.727-2.305-1.929-2.305-.742 0-1.328.347-1.499.889h-.063V3.983h-1.29V11h1.27v-.791h.064c.21.532.776.86 1.499.86zm-.43-1.025c-.66 0-1.113-.518-1.113-1.28V8.12c0-.825.42-1.343 1.098-1.343.684 0 1.075.518 1.075 1.416v.45c0 .888-.386 1.401-1.06 1.401zm-5.583 1.035c.767 0 1.201-.356 1.406-.737h.059V11h1.216V7.519c0-1.314-.947-1.783-2.11-1.783C1.355 5.736.75 6.42.69 7.27h1.216c.064-.323.313-.552.84-.552.527 0 .864.249.864.771v.464H2.346C1.145 7.953.5 8.568.5 9.496c0 .977.693 1.582 1.704 1.582zm.42-.947c-.44 0-.845-.235-.845-.718 0-.395.269-.684.84-.684h.991v.538c0 .503-.444.864-.986.864zm8.897.567c-.577-.4-.9-1.088-.9-1.983v-.65c0-1.42.894-2.338 2.305-2.338 1.352 0 2.119.82 2.139 1.806h-1.187c-.04-.351-.283-.776-.918-.776-.674 0-1.045.517-1.045 1.328v.625c0 .468.121.834.343 1.067l-.737.92z M14.469 9.414a.75.75 0 0 1 .117 1.055l-4 5a.75.75 0 0 1-1.116.061l-2.5-2.5a.75.75 0 1 1 1.06-1.06l1.908 1.907 3.476-4.346a.75.75 0 0 1 1.055-.117z" },
        { id: 4, name: "Gaming", active: false, svg: "M11.119 2.693c.904.19 1.75.495 2.235.98.407.408.779 1.05 1.094 1.772.32.733.599 1.591.805 2.466.206.875.34 1.78.364 2.606.024.815-.059 1.602-.328 2.21a1.42 1.42 0 0 1-1.445.83c-.636-.067-1.115-.394-1.513-.773a11.307 11.307 0 0 1-.739-.809c-.126-.147-.25-.291-.368-.422-.728-.804-1.597-1.527-3.224-1.527-1.627 0-2.496.723-3.224 1.527-.119.131-.242.275-.368.422-.243.283-.494.576-.739.81-.398.378-.877.705-1.513.772a1.42 1.42 0 0 1-1.445-.83c-.27-.608-.352-1.395-.329-2.21.024-.826.16-1.73.365-2.606.206-.875.486-1.733.805-2.466.315-.722.687-1.364 1.094-1.772.486-.485 1.331-.79 2.235-.98.932-.196 2.03-.292 3.119-.292 1.089 0 2.187.096 3.119.292zm-6.032.979c-.877.185-1.469.443-1.733.708-.276.276-.587.783-.885 1.465a13.748 13.748 0 0 0-.748 2.295 12.351 12.351 0 0 0-.339 2.406c-.022.755.062 1.368.243 1.776a.42.42 0 0 0 .426.24c.327-.034.61-.199.929-.502.212-.202.4-.423.615-.674.133-.156.276-.323.44-.505C4.861 9.97 5.978 9.026 8 9.026s3.139.943 3.965 1.855c.164.182.307.35.44.505.214.25.403.472.615.674.318.303.601.468.929.503a.42.42 0 0 0 .426-.241c.18-.408.265-1.02.243-1.776a12.354 12.354 0 0 0-.339-2.406 13.753 13.753 0 0 0-.748-2.295c-.298-.682-.61-1.19-.885-1.465-.264-.265-.856-.523-1.733-.708-.85-.179-1.877-.27-2.913-.27-1.036 0-2.063.091-2.913.27z M11.5 6.026a.5.5 0 1 1-1 0 .5.5 0 0 1 1 0zm-1 1a.5.5 0 1 1-1 0 .5.5 0 0 1 1 0zm2 0a.5.5 0 1 1-1 0 .5.5 0 0 1 1 0zm-1 1a.5.5 0 1 1-1 0 .5.5 0 0 1 1 0zm-7-2.5h1v3h-1v-3z M3.5 6.526h3v1h-3v-1zM3.051 3.26a.5.5 0 0 1 .354-.613l1.932-.518a.5.5 0 0 1 .258.966l-1.932.518a.5.5 0 0 1-.612-.354zm9.976 0a.5.5 0 0 0-.353-.613l-1.932-.518a.5.5 0 1 0-.259.966l1.932.518a.5.5 0 0 0 .612-.354z" },
        { id: 5, name: "Frivilig", active: false, svg: "M8 2.748l-.717-.737C5.6.281 2.514.878 1.4 3.053c-.523 1.023-.641 2.5.314 4.385.92 1.815 2.834 3.989 6.286 6.357 3.452-2.368 5.365-4.542 6.286-6.357.955-1.886.838-3.362.314-4.385C13.486.878 10.4.28 8.717 2.01L8 2.748zM8 15C-7.333 4.868 3.279-3.04 7.824 1.143c.06.055.119.112.176.171a3.12 3.12 0 0 1 .176-.17C12.72-3.042 23.333 4.867 8 15z" },
        { id: 6, name: "Skolehjælp", active: false, svg: "M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168l10-10zM11.207 2.5L13.5 4.793 14.793 3.5 12.5 1.207 11.207 2.5zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293l6.5-6.5zm-9.761 5.175l-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325z" }]

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
    queries = Array<Query>();

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
        const newQuery = {} as Query;
        newQuery.name = name;
        newQuery.value = value;
        this.queries = this.$store.state.queries;
        this.queries = this.DeleteFromQueryList(this.queries, name);
        this.queries.push(newQuery);
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
            this.queries = this.DeleteFromQueryList(this.queries, "categoryId");
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
            this.queries = this.DeleteFromQueryList(this.queries, "region");
        }
        this.ReplaceRoute(this.queries);
    }

    DeleteFromQueryList(array: Array<Query>, name: string) {
        for (let i = array.length - 1; i >= 0; i--) {
            if (array[i].name === name) {
                array.splice(i, 1);
            }
        }
        return array;
    }

    ReplaceRoute(parameters: Array<Query>) {
        if (this.$route.name === "Overview") {
            this.$router.replace({ query: { payload: JSON.stringify(parameters) } });
        } else {
            this.$router.replace({ name: "Overview", query: { payload: JSON.stringify(parameters) } });
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
