<template>
  <div>
      <h1>Profil</h1>
      Velkommen, {{ user.firstName }}
      <button @click="LogOut">log out</button>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { GetLoggedInId, GetBrugerById, Logout } from "../api/user";
import { User } from "../types/user";

@Component
export default class Home extends Vue {
    user = {} as User;

    LogOut() {
        Logout();
        this.$router.push({ name: "Login" });
    }

    mounted() {
        GetBrugerById(GetLoggedInId()).then(response => {
            this.user = response.data;
        });
    }
}
</script>
