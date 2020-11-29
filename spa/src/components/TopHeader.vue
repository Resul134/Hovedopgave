<template>
  <div id="rootDiv">
    <b-navbar toggleable="lg" type="dark" class="header-style" sticky>
      <b-navbar-brand href="#">Workio</b-navbar-brand>
      <div class="search-bar-div">
        <b-form-input placeholder="Search" class="search-bar" v-model="searchBarInput"></b-form-input>
      </div>
      <router-link to="/register" class="opret-arbejdsopgaver-button">
            <b-button size="md" >Opret arbejdsopgaver</b-button>
      </router-link>
      <div class="login-div">
        <div v-if="loggedIn">
          <router-link to="/">
          <!-- icon for profile -->
         <svg width="3em" height="3em" viewBox="0 0 16 16" class="bi bi-person-circle" fill="white" xmlns="http://www.w3.org/2000/svg">
          <path d="M13.468 12.37C12.758 11.226 11.195 10 8 10s-4.757 1.225-5.468 2.37A6.987 6.987 0 0 0 8 15a6.987 6.987 0 0 0 5.468-2.63z"/>
          <path fill-rule="evenodd" d="M8 9a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"/>
          <path fill-rule="evenodd" d="M8 1a7 7 0 1 0 0 14A7 7 0 0 0 8 1zM0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8z"/>
        </svg>
          </router-link>
        </div>
        <div v-else>
          <router-link to="/">
          <!-- icon for log in -->
          <svg width="3em" height="3em" viewBox="0 0 16 16" class="bi bi-box-arrow-in-up" fill="white" xmlns="http://www.w3.org/2000/svg">
            <path fill-rule="evenodd" d="M3.5 10a.5.5 0 0 1-.5-.5v-8a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 .5.5v8a.5.5 0 0 1-.5.5h-2a.5.5 0 0 0 0 1h2A1.5 1.5 0 0 0 14 9.5v-8A1.5 1.5 0 0 0 12.5 0h-9A1.5 1.5 0 0 0 2 1.5v8A1.5 1.5 0 0 0 3.5 11h2a.5.5 0 0 0 0-1h-2z"/>
            <path fill-rule="evenodd" d="M7.646 4.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708L8.5 5.707V14.5a.5.5 0 0 1-1 0V5.707L5.354 7.854a.5.5 0 1 1-.708-.708l3-3z"/>
          </svg>
          </router-link>
        </div>
      </div>
    </b-navbar>
  </div>
</template>
// https://icons.getbootstrap.com/ or https://mdbootstrap.com/docs/vue/content/icons-list/
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class TopHeader extends Vue {
  loggedIn = {
      type: Boolean,
      default: null,
      required: true
  };//  get the property value from mother component, or by emitting the value. Currently just local variable

  searchBarInput = "";
}
</script>

<style lang="scss" scoped>
@import "@/assets/main.scss";

.header-style{
    background-color: $primary;
}

.search-bar-div {
  padding-right: 50px;
  padding-left: 50px;
  width: 90%;
}
.opret-arbejdsopgaver-button{
}
.login-div {
    padding-left: 40px;
}
.header {
  position: fixed; /* fixing the position takes it out of html flow - knows
                   nothing about where to locate itself except by browser
                   coordinates */
  left:0;           /* top left corner should start at leftmost spot */
  top:0;            /* top left corner should start at topmost spot */
  width:100vw;      /* take up the full browser width */
  z-index:1020;  /* high z index so other content scrolls underneath */
  height:100px;     /* define height for content */
}
</style>
