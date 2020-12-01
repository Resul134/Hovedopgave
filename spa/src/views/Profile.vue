<template>
  <div>
      <h1>Profil</h1>
      <div id="userInfo" class="user-info">
          <b-row>
              <b-col>
                  <div class="profile">
                      <h4 style="text-align:center;">Profil information</h4>
                    <div class="content-flex">
                      <b>Username: </b>
                      <b-form-input v-model="userUsername" :state="userNameState()"></b-form-input>
                  </div>
                  <div class="content-flex">
                      <b>Password: </b>
                      <b-form-input v-model="userPassword" type="password" id="input-live-password" :state="passwordState()"></b-form-input>
                  </div>
                  <div class="content-flex">
                      <b>Password repeat: </b>
                      <b-form-input v-model="userPasswordRepeat" type="password" id="input-live-password" :state="passwordRepeatState()"></b-form-input>
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
                  </div>
                  <div class="content-flex">
                      <b>Email: </b>
                      <b-form-input v-model="userEmail" :state="emailState()"></b-form-input>
                  </div>
                  <div class="content-flex">
                      <b-button variant="danger" @click="deleteProfil">Slet konto</b-button>
                      <b-button @click="RedigerProfil">Bekræft</b-button>
                  </div>
                  </div>
              </b-col>
              <b-col></b-col>
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
    // LogOut() {
    //     Logout();
    //     this.$router.push({ name: "Login" });
    // }

    phoneState() {
        if (this.userPhone.length < 8 || this.userPhone.length > 8) {
            return false;
        }
    }

    passwordState() {
        if (this.userPassword.length ! < 8) {
            return false;
        }
    }

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
        if (this.userPassword === "" || this.userEmail === "" || this.userUsername === "" || this.userPassword === "" || this.userPhone.length < 8 || this.userFirstName === "" || this.userLastName === "") {
            alert("Der gik noget galt! Der må ikke være tomme felter");
        } else {
            if (confirm("Vil du bekræfte ændringerne?")) {
                RedigerBruger(GetLoggedInId(), this.userFirstName, this.userLastName, this.userBirthday, this.userGender, parseInt(this.userPhone), this.userEmail, this.userUsername, this.userPassword).then(response => {
                    if (response.status === 200) {
                        console.log("Ændring succesfuld. Status kode = " + response.status);
                    }
                }).catch(() => {
                    console.log("Noget gik galt");
                });
            }
        }
    }

    mounted() {
        GetBrugerById(GetLoggedInId()).then(response => {
            this.user = response.data;
            this.userUsername = response.data.username;
            this.userPassword = response.data.password;
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
    border-radius: 5px;
    border-bottom: 1px solid black;
    margin-top: 5px;
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
