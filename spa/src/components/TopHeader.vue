<template>
<div id="rootDiv">
    <b-navbar toggleable="lg" type="dark" variant="info">
      <b-navbar-brand href="#">Workio</b-navbar-brand>
      <b-form-input placeholder="Search" class="search-bar"></b-form-input>
    </b-navbar>
</div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class TopHeader extends Vue {
}
</script>

<style lang="scss" scoped>

.search-bar{
}

button{
}

.topheader-style{
    background-color: mediumblue;
}
</style>
