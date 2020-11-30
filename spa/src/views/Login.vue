<template>
  <div class="login">
    <h1>Log ind</h1>
    <b-alert v-if="loggedIn" variant="success" show>Logged in</b-alert>
    <b-alert v-if="error" variant="danger" show>Der skete en fejl.</b-alert>
    <p>Brugernavn</p>
    <b-form-input v-model="username"></b-form-input>
    <p>Kodeord</p>
    <b-form-input type="password" v-model="password"></b-form-input>
    <div class="d-flex mt-4">
        <b-button @click="Logind()" class="ml-auto" variant="primary">Log ind</b-button>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { GetUserByLogin } from "../api/user";

@Component
export default class Login extends Vue {
    username = "";
    password = "";
    error = false;
    loggedIn = false;

    Logind() {
        if (this.username === "" || this.password === "") return;

        GetUserByLogin(this.username, this.password).then(response => {
            if (response.data.id !== 0) {
                localStorage.setItem("user", response.data.id);
                this.loggedIn = true;
                this.error = false;
                this.$router.push({ name: "Profile" });
            } else {
                localStorage.removeItem("user");
                this.error = true;
                this.loggedIn = false;
            }
        });
    }
}
</script>

<style scoped lang="scss">
@import "../assets/main.scss";
.login {
    width: 700px;
    max-width: 92%;
    margin: 0 auto;
    background: $light;
    padding:40px;
    border-radius: 12px;
}
p {
    margin-bottom: 0px;
    font-weight: bold;
}
h1 {
    margin-bottom: 35px;
    font-weight: bold;
}
input[type="text"], #birthday__outer_ {
    margin-bottom: 20px;
}
</style>
