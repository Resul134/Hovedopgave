<template>
  <div class="register">
    <h1>Opret bruger</h1>
    <b-alert v-if="created" variant="success" show>Din bruger blev oprettet! Gå til <a href="#/login">Log ind</a></b-alert>
    <b-alert v-if="error" variant="danger" show>Der skete en fejl.</b-alert>
    <div class="flex">
        <div>
            <p>Fornavn</p>
            <b-form-input v-model="firstName"></b-form-input>
        </div>
        <div>
            <p>Efternavn</p>
            <b-form-input v-model="lastName"></b-form-input>
        </div>
    </div>
    <div class="flex">
        <div>
            <p>Email</p>
            <b-form-input v-model="email"></b-form-input>
        </div>
        <div>
            <p>Telefon</p>
            <b-form-input v-model="phone" type="number"></b-form-input>
        </div>
    </div>
    <p>Fødselsdag</p>
    <b-form-datepicker id="birthday" v-model="birthday"></b-form-datepicker>
    <p>Køn</p>
    <b-form-select v-model="selectedGender" :options="genderOptions"></b-form-select>
    <div class="flex mt-3">
        <div>
            <p>Brugernavn</p>
            <b-form-input v-model="username"></b-form-input>
        </div>
        <div>
            <p>Kodeord</p>
            <b-form-input type="password" v-model="password"></b-form-input>
            <p class="mt-2">Kodeord igen</p>
            <b-form-input type="password" v-model="passwordRepeat"></b-form-input>
        </div>
    </div>
    <div class="d-flex mt-4">
        <b-button @click="Opret()" class="ml-auto" variant="primary">Opret</b-button>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { OpretBruger } from "../api/user";

@Component
export default class Home extends Vue {
    genderOptions = ["Mand", "Kvinde", "Anden"];
    firstName = "";
    lastName = "";
    email = "";
    phone = "";
    birthday: Date = new Date();
    selectedGender = "Mand";
    username = "";
    password = "";
    passwordRepeat = "";

    created = false;
    error = false;

    Opret() {
        if (this.password === this.passwordRepeat) {
            if (this.firstName === "" && this.lastName === "" && this.email === "" && this.phone === "" && this.username === "" && this.password === "") {
                this.error = true;
                return;
            }
            OpretBruger(this.firstName, this.lastName, this.birthday, this.selectedGender, parseInt(this.phone), this.email, this.username, this.password).then(response => {
                if (response.status === 200) {
                    this.firstName = "";
                    this.lastName = "";
                    this.email = "";
                    this.phone = "";
                    this.birthday = new Date();
                    this.selectedGender = "Mand";
                    this.username = "";
                    this.password = "";
                    this.passwordRepeat = "";

                    this.error = false;
                    this.created = true;
                }
            }).catch(() => {
                this.error = true;
            });
        } else {
            this.error = true;
        }
    }
}
</script>

<style scoped lang="scss">
@import "../assets/main.scss";
.flex {
    display: flex;

    div {
        width: 50%;

        &:first-of-type {
            margin-right: 5%;
        }
    }
}
.register {
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
