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
            <b-img src="../assets/profileicon.png"></b-img>
          </router-link>
        </div>
        <div v-else>
          <router-link to="/">
             <b-img src="../assets/login.png"></b-img>
          </router-link>
        </div>
      </div>
    </b-navbar>
  </div>
</template>

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

//   checkRoute(route: string) {
//       if (route === "opgaver") {
//           this.$router.push({ path: "/" });
//       } else if (route === "login") {
//           this.$router.push({ path: "/" });
//       } else if (route === "profile") {
//           this.$router.push({ path: "/" });
//       } else {
//           console.log("Routing error");
//       }
//   };
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
    width: 10%;
}
.login-div {
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
