<template>
    <div>
        <b-row>
            <b-col>
                <div class="seeMore">
                    <p class="tag">{{ price }} kr</p>
                    <h1 class="title">{{title}}</h1>
                    <p>{{description}} </p>
                </div>
                <div class="d-flex" style="flex-direction: column">
                    <h1 class="mt-4">Kommentarer</h1>
                    <b-textarea placeholder="Skriv her.." rows="10"></b-textarea>
                    <button class="btn btn-primary mt-3 ml-auto">Skriv kommentar</button>
                </div>
            </b-col>

            <b-col cols="3">
                <div v-if="this.$store.state.loggedIn">
                    <div v-if="!isTaskCreator">
                        <button class="mb-3 btn btn-danger" v-if="signedUp" @click="dropOut()" style="width:100%;">Afmeld</button>
                        <button class="mb-3 btn btn-primary" v-else @click="signUp()" style="width:100%;">Tilmeld</button>
                    </div>
                </div>
                <button v-else class="mb-3 btn btn-primary" @click="routeLogin()" style="width:100%;">Login for at tilmelde</button>
                <div class="seeMore">
                    <p class="font-weight-bold text-center">Arbejdsgiver<p/>
                    <p>
                        <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-person-circle" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                            <path d="M13.468 12.37C12.758 11.226 11.195 10 8 10s-4.757 1.225-5.468 2.37A6.987 6.987 0 0 0 8 15a6.987 6.987 0 0 0 5.468-2.63z"/>
                            <path fill-rule="evenodd" d="M8 9a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"/>
                            <path fill-rule="evenodd" d="M8 1a7 7 0 1 0 0 14A7 7 0 0 0 8 1zM0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8z"/>
                        </svg>
                        {{firstName}} {{ lastName }}
                    </p>
                    <p>
                        <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-envelope-fill" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" d="M.05 3.555A2 2 0 0 1 2 2h12a2 2 0 0 1 1.95 1.555L8 8.414.05 3.555zM0 4.697v7.104l5.803-3.558L0 4.697zM6.761 8.83l-6.57 4.027A2 2 0 0 0 2 14h12a2 2 0 0 0 1.808-1.144l-6.57-4.027L8 9.586l-1.239-.757zm3.436-.586L16 11.801V4.697l-5.803 3.546z"/>
                        </svg>
                        {{userMail}}
                    </p>
                    <p>
                        <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-telephone-fill" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" d="M2.267.98a1.636 1.636 0 0 1 2.448.152l1.681 2.162c.309.396.418.913.296 1.4l-.513 2.053a.636.636 0 0 0 .167.604L8.65 9.654a.636.636 0 0 0 .604.167l2.052-.513a1.636 1.636 0 0 1 1.401.296l2.162 1.681c.777.604.849 1.753.153 2.448l-.97.97c-.693.693-1.73.998-2.697.658a17.47 17.47 0 0 1-6.571-4.144A17.47 17.47 0 0 1 .639 4.646c-.34-.967-.035-2.004.658-2.698l.97-.969z"/>
                        </svg>
                        {{userNumber}}
                    </p>
                </div>
                <div class="seeMore mt-3">
                    <p class="font-weight-bold text-center">Praktisk<p/>
                    <p><strong>Oprettet: </strong>{{ date | formatDate }}</p>
                    <p><strong>Region: </strong>{{region}}</p>
                    <p><strong >Status: </strong><span :class="status">{{status}}</span></p>
                </div>
                 <b-button class="tilmeldt-button" to="/assignedUsers" v-if="isTaskCreator">Tilmeldte brugere</b-button>
            </b-col>
        </b-row>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { GetTaskById } from "../api/task";
import { GetBrugerById, GetLoggedInId } from "../api/user";
import { GetAssignedUserMatch, OpretAssignedUser, DeleteAssignedUser } from "../api/assignedUser";
import { Task } from "../types/task";
import { User } from "../types/user";
import { AssignedUser } from "../types/assignedUser";

@Component
export default class SeeMore extends Vue {
    task = {} as Task;
    date: Date = new Date();
    title = "";
    price = "";
    description = "";
    region = "";
    status = "";
    userID = 3;

    user = {} as User;
    firstName = "";
    lastName = "";
    userMail = "";
    userNumber = "";

    assignedUser = {} as AssignedUser;
    signedUp = false;
    isTaskCreator = false;

    mounted() {
        if (this.$store.state.taskID === null) {
            this.$router.push({ name: "About" });
        } else {
            GetTaskById(this.$store.state.taskID).then(response => {
                this.task = response.data;
                this.region = response.data.region;
                this.status = response.data.status;
                this.date = response.data.date;
                this.title = response.data.title;
                this.price = response.data.price;
                this.description = response.data.description;
            });
            GetBrugerById(this.$store.state.userID).then(response => {
                this.firstName = response.data.firstName;
                this.lastName = response.data.lastName;
                this.userMail = response.data.email;
                this.userNumber = response.data.phone;
            });

            if (this.$store.state.loggedIn) {
                if (GetLoggedInId() === this.$store.state.userID.toString()) {
                    this.isTaskCreator = true;
                } else {
                    this.setup();
                }
            }
        }
    }

    setup() {
        GetAssignedUserMatch(this.$store.state.taskID, GetLoggedInId()).then(response => {
            if (response.data.id !== 0) {
                this.assignedUser = response.data;
                this.signedUp = true;
            } else {
                this.assignedUser = {} as AssignedUser;
                this.signedUp = false;
            }
        });
    }

    signUp() {
        OpretAssignedUser(this.$store.state.taskID, GetLoggedInId(), false).then(response => {
            if (response.data) {
                this.setup();
            }
        });
    }

    dropOut() {
        DeleteAssignedUser(this.assignedUser.id).then(response => {
            if (response.data) {
                this.setup();
            }
        });
    }

    routeLogin() {
        this.$router.push({ name: "Login" });
    }
}
</script>

<style lang="scss" scoped>
@import "@/assets/main.scss";
svg {
    margin-top: -3px;
    font-size: 120%;
    margin-right: 4px;
}

.seeMore{
    border-radius: 7px;
    padding: 25px;
    position: relative;
    background: $light;
}

.tilmeldt-button {
    width: 100%;
    background-color: $primary;
}

.tag {
    background: $primary;
    font-weight: 700;
    padding: 5px;
    padding-left: 10px;
    color: white;
    border-radius: 0 7px 0 7px;
    min-width: 100px;
    display: inline-block;
    position: absolute;
    top: 0px;
    right: 0px;
    text-align: center;
}

.font-weight-bold {
    border-bottom: 1px solid darken($light, 10);
    padding-bottom: 10px;
    margin-bottom: 20px;
}

h1 {
    font-weight: bold;
    font-size: 30px;

    &.title {
        font-size: 24px;
        margin-bottom: 25px;
    }
}

.Løst {
    color: #dc3545;
}

.Aktiv {
    color: #ffab00;
}

.Ledig {
    color: #28a745;
}
</style>
