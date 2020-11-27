<template>
  <div id="rootDiv">
    <b-navbar toggleable="lg" type="dark" variant="primary" sticky>
      <b-navbar-brand href="#">Workio</b-navbar-brand>
      <div class="search-bar-div">
        <b-form-input placeholder="Search" class="search-bar" v-model="searchBarInput"></b-form-input>
      </div>
      <b-button size="md" class="opret-arbejdsopgaver-button" @click="checkRoute('opgaver')">Opret arbejdsopgaver</b-button>
      <div class="login-div">
        <div v-if="loggedIn">
          <a @click="checkRoute('profile')">
            <b-img src="../assets/profileicon.png"></b-img>
          </a>
        </div>
        <div v-else>
          <a @click="checkRoute('login')">
             <b-img src="../assets/login.png"></b-img>
          </a>
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

  changeRoute = "opgaver";
  searchBarInput = "";

  redirectToArbedsOpgaver() {
      this.$router.push({ path: "/" });
  }; // Change path to arbejdsopgaver

  checkRoute(route: string) {
      if (route === "opgaver") {
          this.$router.push({ path: "/" });
      } else if (route === "login") {
          this.$router.push({ path: "/" });
      } else if (route === "profile") {
          this.$router.push({ path: "/" });
      } else {
          console.log("Routing error");
      }
  };
}
</script>

<style lang="scss" scoped>
.search-bar-div {
  padding-right: 50px;
  padding-left: 50px;
  width: 90%;
}
.opret-arbejdsopgaver-button{
  margin-right: 30px;
  width: 14%;
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
