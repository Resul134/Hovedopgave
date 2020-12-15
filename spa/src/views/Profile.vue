<template>
  <div class="rediger">
         <h1>Profil</h1>
    <b-alert v-if="tommefelter" variant="danger" show>Alle felter skal fyldes ud!</b-alert>
    <b-alert v-if="error" variant="danger" show>Oplysninger blev ikke opdateret - Server fejl.</b-alert>
    <b-alert v-if="edited" variant="success" show>Dine oplysninger er blevet opdateret!</b-alert>
    <div>
            <p>Brugernavn</p>
            <b-form-input v-model="userUsername" :state="userNameState()"></b-form-input>
        </div>
    <div class="flex">
        <div>
            <p>Kodeord</p>
            <b-form-input v-model="userPassword" id="input-live-password" :state="passwordState()"></b-form-input>
            <b-form-invalid-feedback id="input-live-password">Kodeord skal være mindst 8 karakterer</b-form-invalid-feedback>
        </div>
        <div>
            <p>Gentag kodeord</p>
            <b-form-input v-model="userPasswordRepeat" :state="passwordRepeatState()"></b-form-input>
        </div>
    </div>
    <div class="flex">
        <div>
            <p>Fornavn</p>
            <b-form-input v-model="userFirstName"></b-form-input>
        </div>
        <div>
            <p>Efternavn</p>
            <b-form-input v-model="userLastName"></b-form-input>
        </div>
    </div>
    <div style="padding-bottom: 15px;">
            <p>Fødselsdato</p>
            <b-form disabled class="custom-form">{{ userBirthday | formatDate }}</b-form>
        </div>
    <div class="flex">
        <div>
            <p>Køn</p>
            <b-form class="custom-form">{{ userGender }}</b-form>
        </div>
        <div>
            <p>Telefon</p>
            <b-form-input v-model="userPhone" :state="phoneState()"></b-form-input>
        </div>
    </div>
    <p>Email</p>
    <b-form-input v-model="userEmail"></b-form-input>
    <p>Beskrivelse</p>
    <div>
        <b-textarea v-model="description" :state="descriptionState()" rows="5"></b-textarea>
    </div>
    <div class="d-flex mt-4">
        <b-button variant="danger" @click="deleteProfil()">Slet konto</b-button>
        <b-button @click="RedigerProfil()" class="ml-auto" variant="primary">Bekræft</b-button>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { GetLoggedInId, GetBrugerById, DeleteBrugerById, RedigerBruger, Logout } from "../api/user";
import { RemoveAssignedUser } from "../api/assignedUser";
import { DeleteAllTaskByUserID } from "../api/task";
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
    currentUserPassword = "";
    userPasswordRepeat = "";
    description = "";
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

    passwordState() {
        if (this.userPassword === "") {

        } else if (this.userPassword.length < 8) {
            return false;
        }
    }

    passwordRepeatState() {
        if (this.userPassword === "" && this.userPassword.length < 1) {
        } else if (this.userPassword === this.userPasswordRepeat) {
            return true;
        } else if (this.userPasswordRepeat !== this.userPassword && this.userPassword.length > 0) {
            return false;
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

    descriptionState() {
        if (this.description === "") {
            return false;
        }
    }

    deleteProfil() {
        if (confirm("Er du sikker? Handlingen kan ikke trækkes tilbage.")) {
            DeleteBrugerById(GetLoggedInId()).then(response => {
                console.log("Din konto er slettet. Status kode = " + response.status);
                this.$store.commit("loggedInState", false);
                this.$router.push({ name: "Home" });
            }).then(() => {
                DeleteAllTaskByUserID(GetLoggedInId()).then(response => {
                    console.log("Opgaver bundet til kontoen er slettet. Status code: " + response.status);
                });
            }).then(() => {
                RemoveAssignedUser(GetLoggedInId()).then(response => {
                    console.log("Assigned user removed: " + response.status);
                    Logout();
                });
            }).catch(() => {
                this.error = true;
                this.tommefelter = false;
                this.edited = false;
                console.log("Couldn't complete the action.");
            });
        }
    }

    RedigerProfil() {
        if (this.userEmail === "" || this.userUsername === "" || this.userPhone.length < 8 || this.userFirstName === "" || this.userLastName === "" || this.userPassword.length < 8 || this.description === "") {
            this.tommefelter = true;
            this.edited = false;
            this.error = false;
        } else {
            if (confirm("Vil du bekræfte ændringerne?")) {
                RedigerBruger(GetLoggedInId(), this.userFirstName, this.userLastName, this.userBirthday, this.userGender, parseInt(this.userPhone), this.userEmail, this.userUsername, this.userPassword, this.description).then(response => {
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
            this.userGender = response.data.gender;
            this.userPhone = response.data.phone;
            this.userEmail = response.data.email;
            this.userBirthday = response.data.birthday;
            this.description = response.data.description;
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

.profile{
    border-radius: 7px;
    padding: 10px;
    background: $light;
}
.custom-form{
    background-color:white;
    height: calc(1.5em + 0.75rem + 2px);
    border-radius:0.25rem;
    border: 1px solid #ced4da;
    line-height: 1.5;
    background-clip: padding-box;
    background-color: #fff;
    color: #495057;
    padding: 0.375rem 0.75rem;
    font-size: 1rem;
    font-weight: 400;
}

.flex {
    display: flex;

    div {
        width: 50%;

        &:first-of-type {
            margin-right: 5%;
        }
    }
}
.boxHeight{
    height: 200px;
}
.rediger {
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
