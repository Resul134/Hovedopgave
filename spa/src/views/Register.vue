<template>
  <div class="register">
    <h1>Opret bruger</h1>
    <b-alert variant="success" show>Din bruger blev oprettet! Gå til <router-link to="/login">Log ind</router-link></b-alert>
    <b-alert v-if="error" variant="danger" show>Der skete en fejl.</b-alert>
    <b-row>
        <b-col md=12 xl=6>
            <p>Fornavn</p>
            <b-form-input v-model="firstName"></b-form-input>
        </b-col>
        <b-col md=12 xl=6>
            <p>Efternavn</p>
            <b-form-input v-model="lastName"></b-form-input>
        </b-col>
    </b-row>
    <b-row>
        <b-col md=12 xl=6>
            <p>Email</p>
            <b-form-input v-model="email"></b-form-input>
        </b-col>
        <b-col md=12 xl=6>
            <p>Telefon</p>
            <b-form-input v-model="phone" type="number"></b-form-input>
        </b-col>
    </b-row>
    <p style="margin-top: 20px;">Fødselsdag</p>
    <b-form-datepicker id="birthday" v-model="birthday"></b-form-datepicker>
    <p>Køn</p>
    <b-form-select v-model="selectedGender" :options="genderOptions"></b-form-select>
    <b-row style="margin-top: 20px;">
        <b-col md=12 xl=6>
            <p>Brugernavn</p>
            <b-form-input v-model="username"></b-form-input>
        </b-col>
        <b-col md=12 xl=6>
            <p>Kodeord</p>
            <b-form-input type="password" v-model="password"></b-form-input>
            <p class="mt-2">Kodeord igen</p>
            <b-form-input type="password" v-model="passwordRepeat"></b-form-input>
        </b-col>
    </b-row>
    <p style="margin-top: 20px;">Beskrivelse</p>
    <div>
        <b-textarea v-model="description" rows="5"></b-textarea>
    </div>
    <p>Kompetencer</p>
    <div class="flex">
    <b-form-input v-model="skill"></b-form-input>
    <b-button @click="addSkill()" class="ml-auto, btn btn-secondary btn-sm, buttonStyle">Tilføj</b-button>
    </div>
    <ul>
    <li v-for="(skill, index) in skillList" :key="skill">{{ skill }} <b-button @click="deleteSkill(index)" class="ml-auto, btn btn-secondary btn-sm" variant="danger">X</b-button></li>
    </ul>
    <div class="d-flex mt-4">
        <b-button @click="Opret()" class="ml-auto" variant="primary">Opret</b-button>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { OpretBruger, GetUserByUsername } from "../api/user";
import { OpretQualification } from "../api/qualification";

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
    description = "";

    skillList = Array<string>();
    userID = 0;
    skill = "";

    created = false;
    error = false;

    addSkill() {
        if (this.skill !== "") {
            this.skillList.push(this.skill);
        }
        this.skill = "";
    }

    deleteSkill(currentIndex: number) {
        this.skillList.splice(currentIndex, 1);
    }

    Opret() {
        if (this.password === this.passwordRepeat) {
            if (this.firstName === "" && this.lastName === "" && this.email === "" && this.phone === "" && this.username === "" && this.password === "") {
                this.error = true;
                return;
            }
            OpretBruger(this.firstName, this.lastName, this.birthday, this.selectedGender, parseInt(this.phone), this.email, this.username, this.password, this.description).then(response => {
                if (response.status === 200) {
                    GetUserByUsername(this.username).then(response => {
                        this.userID = response.data.id;
                        console.log(this.userID);
                        this.skillList.forEach((skill, index) => {
                            OpretQualification(this.userID, this.skillList[index]).then(response => {
                                if (response.status === 200) {
                                    this.error = false;
                                    this.created = true;
                                }
                            }).catch(() => {
                                this.error = true;
                            });
                        });
                        this.skillList = [];
                    });
                    this.firstName = "";
                    this.lastName = "";
                    this.email = "";
                    this.phone = "";
                    this.birthday = new Date();
                    this.selectedGender = "Mand";
                    this.username = "";
                    this.password = "";
                    this.passwordRepeat = "";
                    this.description = "";
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
.boxHeight{
    height: 200px;
}
.register {
    width: 700px;
    max-width: 92%;
    margin: 0 auto;
    background: $light;
    padding:40px;
    border-radius: 12px;
}
ul {
    display: flex;
    list-style: none;
    margin-bottom: 6rem;
}

li {
    background-color:darkgray;
    margin-right: 15px;
    padding: 6px;
    border-radius: 7px;
}
.btn-sm, .btn-group-sm > .btn {
    font-size: 0.975rem;
    line-height: 1;
    font-size: 0.6rem;
}
textarea{
    margin-bottom: 20px;
}

.buttonStyle {
    margin-left: 10px;
    height: 40px;
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
