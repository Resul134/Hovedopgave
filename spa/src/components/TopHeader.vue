<template>
<div id="rootDiv">
    <b-navbar toggleable="lg" type="dark" variant="info">
      <b-navbar-brand href="#">Top Header Example</b-navbar-brand>
      <b-navbar-toggle target="nav-collapse" v-b-toggle.sidebar-no-header></b-navbar-toggle>
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
    max-width: 800px;
}
</style>
