<template>
  <div id="rootDiv">
    <b-navbar toggleable="lg" type="dark" class="header">
        <b-row style="width: 100%;">
              <b-col style="flex-grow: 0" cols="1">
          <b-navbar-brand to="/">Workio</b-navbar-brand>
              </b-col>
      <b-col cols="8"
        ><div>
          <b-form-input
            placeholder="Søg"
            class="search-bar"
            v-model="searchBarInput"
          ></b-form-input></div
      ></b-col>
      <b-col md="auto">
          <b-button size="sm" to="/" v-if="loggedIn">Opret arbejdsopgaver</b-button>
        <div>
          <div v-if="!loggedIn">
            <b-dropdown>
              <template #button-content>
                  <svg width="2.2em" height="2.2em" viewBox="0 0 16 16" class="bi bi-person-circle" fill="white" xmlns="http://www.w3.org/2000/svg">
                  <path d="M13.468 12.37C12.758 11.226 11.195 10 8 10s-4.757 1.225-5.468 2.37A6.987 6.987 0 0 0 8 15a6.987 6.987 0 0 0 5.468-2.63z"/>
                  <path fill-rule="evenodd" d="M8 9a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"/>
                  <path fill-rule="evenodd" d="M8 1a7 7 0 1 0 0 14A7 7 0 0 0 8 1zM0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8z"/>
                </svg>
              </template>
                  <b-dropdown-item to="/login">Login</b-dropdown-item>
                  <b-dropdown-item to="/register">Register</b-dropdown-item>
            </b-dropdown>
          </div>
          <div v-else>
            <b-dropdown>
              <template #button-content>
                  <svg width="2.2em" height="2.2em" viewBox="0 0 16 16" class="bi bi-person-circle" fill="white" xmlns="http://www.w3.org/2000/svg">
                  <path d="M13.468 12.37C12.758 11.226 11.195 10 8 10s-4.757 1.225-5.468 2.37A6.987 6.987 0 0 0 8 15a6.987 6.987 0 0 0 5.468-2.63z"/>
                  <path fill-rule="evenodd" d="M8 9a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"/>
                  <path fill-rule="evenodd" d="M8 1a7 7 0 1 0 0 14A7 7 0 0 0 8 1zM0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8z"/>
                </svg>
              </template>
                  <b-dropdown-item to="/profile">Profile</b-dropdown-item>
                  <b-dropdown-item @click="LogoutUser">Logout</b-dropdown-item>
            </b-dropdown>
          </div>
        </div>
      </b-col>
        </b-row>
    </b-navbar>
  </div>
</template>
// https://icons.getbootstrap.com/ or https://mdbootstrap.com/docs/vue/content/icons-list/
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { GetLoggedInId, Logout } from "../api/user";

@Component
export default class TopHeader extends Vue {
  loggedIn = false; //  get the property value from mother component, or by emitting the value. Currently just local variable
  searchBarInput = "";
  mounted() {
      if (GetLoggedInId() != null) {
          this.loggedIn = true;
          console.log(GetLoggedInId() + " Getloggedin ID");
      }
  };

  LogoutUser() {
      this.loggedIn = false;
      Logout();
      this.$router.push({ name: "/" });
  }
}
</script>

<style lang="scss" scoped>
@import "@/assets/main.scss";

.header-style {
  background-color: $primary;
}

.search-bar-div {
  padding-right: 50px;
  padding-left: 50px;
  width: 90%;
}
.login-div {
  padding-left: 40px;
}
.header {
  position: fixed; /* fixing the position takes it out of html flow - knows
                   nothing about where to locate itself except by browser
                   coordinates */
  left: 0; /* top left corner should start at leftmost spot */
  top: 0; /* top left corner should start at topmost spot */
  width: 100vw; /* take up the full browser width */
  z-index: 1020; /* high z index so other content scrolls underneath */
  height: 100px; /* define height for content */
  background-color: $primary;

}
</style>
