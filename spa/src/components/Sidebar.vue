<template>
<div class="root">
    <div class="px-3 py-2">
        <h2>Kategorier</h2>
        <b-list-group>
            <b-list-group-item v-on:click="filterCategory('1')" href="" v-bind:active="activeOne">Kategori 1</b-list-group-item>
            <b-list-group-item v-on:click="filterCategory('2')" href="" v-bind:active="activeTwo">Kategori 2</b-list-group-item>
            <b-list-group-item v-on:click="filterCategory('3')" href="" v-bind:active="activeThree">Kategori 3</b-list-group-item>
            <b-list-group-item v-on:click="filterCategory('4')" href="" v-bind:active="activeFour">Kategori 4</b-list-group-item>
            <b-list-group-item v-on:click="filterCategory('5')" href="" v-bind:active="activeFive">Kategori 5</b-list-group-item>
        </b-list-group>
        <h2>Region</h2>
        <b-list-group>
            <b-list-group-item v-on:click="filterRegion('1')" href="" v-bind:active="activeRegionOne">Sjælland</b-list-group-item>
            <b-list-group-item v-on:click="filterRegion('2')" href="" v-bind:active="activeRegionTwo">Jylland</b-list-group-item>
            <b-list-group-item v-on:click="filterRegion('3')" href="" v-bind:active="activeRegionThree">Fyn</b-list-group-item>
        </b-list-group>
    </div>
</div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class Sidebar extends Vue {
    activeOne = false;
    activeTwo = false;
    activeThree = false;
    activeFour = false;
    activeFive = false;

    activeRegionOne = false;
    activeRegionTwo = false;
    activeRegionThree = false;

    setCategoryActive() {
        this.activeOne = false;
        this.activeTwo = false;
        this.activeThree = false;
        this.activeFour = false;
        this.activeFive = false;
    }

    setRegionActive() {
        this.activeRegionOne = false;
        this.activeRegionTwo = false;
        this.activeRegionThree = false;
    }

    filterCategory(category: string) {
        this.setCategoryActive();
        switch (category) {
        case "1":
            this.activeOne = true;
            break;
        case "2":
            this.activeTwo = true;
            break;
        case "3":
            this.activeThree = true;
            break;
        case "4":
            this.activeFour = true;
            break;
        case "5":
            this.activeFive = true;
            break;
        }
    }

    filterRegion(region: string) {
        this.setRegionActive();
        switch (region) {
        case "1":
            this.activeRegionOne = true;
            break;
        case "2":
            this.activeRegionTwo = true;
            break;
        case "3":
            this.activeRegionThree = true;
            break;
        }
    }
}
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";

h2 {
    margin: 10px 0 10px 0;
}

.root {
    background: $light;
    position: fixed;
    top: 56px;
    width: 280px;
    height: 100%;
    box-shadow: whitesmoke;
}

</style>
