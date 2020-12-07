<template>
  <div>
      <h1>Profil</h1>
      <div id="userInfo" class="user-info">
          <div style="display: flex;">
            <b-alert v-if="tommefelter" variant="danger" show>Alle felter skal fyldes ud!</b-alert>
            <b-alert v-if="error" variant="danger" show>Oplysninger blev ikke opdateret - Server fejl.</b-alert>
            <b-alert v-if="edited" variant="success" show>Dine oplysninger er blevet opdateret!</b-alert>
          </div>
          <b-row>
              <b-col>
                  <div class="profile">
                      <h4 style="text-align:center;">Profil information</h4>
                    <div class="content-flex">
                      <b>Brugernavn: </b>
                      <b-form-input v-model="userUsername" :state="userNameState()"></b-form-input>
                  </div>
                  <div class="content-flex">
                      <b>Kodeord: </b>
                      <b-form-input v-model="userPassword" id="input-live-password"></b-form-input>
                        <!-- <b-form-invalid-feedback id="input-live-password" tooltip>Kodeord skal være mindst 8 karakterer</b-form-invalid-feedback> -->
                  </div>
                  <div class="content-flex">
                      <b>Gentag kodeord: </b>
                      <b-form-input v-model="userPasswordRepeat" id="input-live-password" :state="passwordRepeatState()"></b-form-input>
                  </div>
                    <div class="content-flex">
                      <b>Fornavn:</b>
                          <b-form-input v-model="userFirstName" :state="forNavnState()"></b-form-input>
                  </div>
                  <div class="content-flex">
                      <b>Efternavn:</b>
                          <b-form-input v-model="userLastName" :state="efterNavnState()"></b-form-input>
                  </div>
                  <div class="content-flex">
                      <b>Fødselsdato:</b>
                      <p>{{ userBirthday | formatDate }}</p>
                  </div>
                  <div class="content-flex">
                      <b>Køn: </b>
                      <p>{{ userGender }}</p>
                  </div>
                  <div class="content-flex">
                      <b>Telefon: </b>
                      <b-form-input v-model="userPhone" :state="phoneState()" id="input-live-phone"></b-form-input>
                    <b-form-invalid-feedback id="input-live-phone" tooltip>Telefon nummer skal være bestående af 8 tal.</b-form-invalid-feedback>
                  </div>
                  <div class="content-flex">
                      <b>Email: </b>
                      <b-form-input v-model="userEmail" :state="emailState()"></b-form-input>
                  </div>
                  <div class="button-flex">
                      <b-button variant="danger" @click="deleteProfil">Slet konto</b-button>
                      <b-button @click="RedigerProfil">Bekræft</b-button>
                  </div>
                  </div>
              </b-col>
              <b-col cols="3"></b-col>
          </b-row>
      </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { GetLoggedInId, GetBrugerById, DeleteBrugerById, RedigerBruger, Logout } from "../api/user";
import { User } from "../types/user";

@Component
export default class Profile extends Vue {
    user = {} as User;
    userUsername = "";
    userPassword = "";
    userFirstName = "";
    userLastName = "";
    userBirthday: Date = new Date();
    userGender = "";
    userPhone = "";
    userEmail = "";
    userPasswordRepeat = "";
    edited = false;
    error = false;
    tommefelter = false;
    // LogOut() {
    //     Logout();
    //     this.$router.push({ name: "Login" });
    // }

    phoneState() {
        if (this.userPhone.length < 8 || this.userPhone.length > 8) {
            return false;
        }
    }

    // passwordState() {
    //     if (this.userPassword.length ! < 8) {
    //         return false;
    //     }
    // }

    passwordRepeatState() {
        if (this.userPassword === this.userPasswordRepeat) {
            return true;
        }
    }

    userNameState() {
        if (this.userUsername === "") {
            return false;
        }
    }

    emailState() {
        if (this.userEmail === "") {
            return false;
        }
    }

    forNavnState() {
        if (this.userFirstName === "") {
            return false;
        }
    }

    efterNavnState() {
        if (this.userLastName === "") {
            return false;
        }
    }

    deleteProfil() {
        if (confirm("Er du sikker? Handlingen kan ikke trækkes tilbage.")) {
            DeleteBrugerById(GetLoggedInId()).then(response => {
                console.log("Din konto er slettet. Status kode = " + response.status);
                this.$store.commit("loggedInState", false);
                this.$router.push({ name: "Home" });
                Logout();
            }).catch(() => {
                console.log("Couldn't get user ID");
            });
        }
    }

    RedigerProfil() {
        if (this.userEmail === "" || this.userUsername === "" || this.userPassword === "" || this.userPhone.length < 8 || this.userFirstName === "" || this.userLastName === "") {
            this.tommefelter = true;
            this.edited = false;
            this.error = false;
        } else {
            if (confirm("Vil du bekræfte ændringerne?")) {
                RedigerBruger(GetLoggedInId(), this.userFirstName, this.userLastName, this.userBirthday, this.userGender, parseInt(this.userPhone), this.userEmail, this.userUsername, this.userPassword).then(response => {
                    if (response.status === 200) {
                        this.edited = true;
                        this.tommefelter = false;
                        this.error = false;
                        console.log("Ændring succesfuld. Status kode = " + response.status);
                    }
                }).catch(() => {
                    this.error = true;
                    this.tommefelter = false;
                    this.edited = false;
                    console.log("Noget gik galt");
                });
            }
        }
    }

    mounted() {
        GetBrugerById(GetLoggedInId()).then(response => {
            this.user = response.data;
            this.userUsername = response.data.username;
            this.userFirstName = response.data.firstName;
            this.userLastName = response.data.lastName;
            this.userBirthday = response.data.birthday;
            this.userGender = response.data.gender;
            this.userPhone = response.data.phone;
            this.userEmail = response.data.email;
        });
    }
}
</script>

<style lang="scss" scoped>
@import "@/assets/main.scss";

.content-flex{
    display: flex;
    justify-content: space-between;
    border-bottom: 1px solid black;
    padding: 7px;
}

.button-flex{
    display: flex;
    justify-content: space-between;
    padding: 7px;
}

.input-flex{
    display: flex;
    justify-content: flex-end;
}
input{
    width: 50%;
}

.profile{
    border-radius: 7px;
    padding: 10px;
    background: $light;
}

</style>
