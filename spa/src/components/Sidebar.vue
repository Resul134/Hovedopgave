<template>
<div class="root">
    <div>
        <h3>Kategorier</h3>
        <b-list-group id="group">
            <b-list-group-item id="item" class="hvr-sweep-to-right" v-for="(category, idx) in categories" :key="idx" @click="filterCategory(category.id)" href="" v-bind:active="category.active">
                {{ category.name }}
            </b-list-group-item>
        </b-list-group>
        <h3 class="region">Region</h3>
        <b-list-group>
            <b-list-group-item id="item" class="hvr-sweep-to-right" v-for="(region, idx) in regions" :key="idx" @click="filterRegion(region.id)" href="" v-bind:active="region.active">
                {{ region.name }}
            </b-list-group-item>
        </b-list-group>
    </div>
</div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class Sidebar extends Vue {
    categories = [
        { id: 1, name: "Udendørs", active: false },
        { id: 2, name: "IT", active: false },
        { id: 3, name: "Korrektur", active: false },
        { id: 4, name: "Gaming", active: false },
        { id: 5, name: "Frivilig", active: false },
        { id: 6, name: "Skolehjælp", active: false }]

    currentCategory = this.categories[0];

    regions = [
        { id: 1, name: "Sjælland", active: false },
        { id: 2, name: "Nordjylland", active: false },
        { id: 3, name: "Syddanmark", active: false },
        { id: 4, name: "Hovedstaden", active: false },
        { id: 5, name: "Midtjylland", active: false }]

    currentRegion = this.regions[0];

    filterCategory(id: number) {
        this.currentCategory.active = false;

        this.categories[id - 1].active = true;
        this.currentCategory = this.categories[id - 1];

        //  Filtering categories needs to be added here
    }

    filterRegion(id: number) {
        this.currentRegion.active = false;

        this.regions[id - 1].active = true;
        this.currentRegion = this.regions[id - 1];

        //  Filtering regions needs to be added here
    }
}
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";

h3 {
    margin: 10px 0 10px 10px;
    font-weight: bold;
}
.region {
    margin: 40px 0 10px 10px;
}

.root {
    background: white;
    position: fixed;
    top: 56px;
    width: 280px;
    height: 100%;
    box-shadow: 5px 0px 5px whitesmoke;
}

#group {
    margin: 0;
    padding: 0;
    width: 100%;
}

#item {
    border-radius: 0;
    border-right: 0;
    font-weight: bold;
}
.hvr-sweep-to-right {
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
