<template>
    <div>
        <b-alert v-if="notChosen" variant="danger" show>Ingen personer valgt til arbejdsopgaven!</b-alert>
        <b-row>
            <b-col cols="9">
                <div class="seeMore">
                    <p class="tag" v-if="!rediger">{{ price }} kr</p>
                    <b-input v-model="prisInput" placeholder="Prisen" v-if="rediger" class="pris">kr</b-input>
                    <h1 class="title" v-if="!rediger">{{title}}</h1>
                    <b v-if="rediger">Titel</b>
                    <b-input placeholder="Titlen" v-model="titleInput" v-if="rediger" class="title"></b-input>
                    <p v-if="!rediger">{{description}} </p>
                    <b v-if="rediger">Beskrivelse</b>
                    <b-textarea v-if="rediger" placeholder="Opgavebeskrivelse" v-model="opgaveBeskrivelseInput"></b-textarea>
                    <div v-if="rediger">
                        <b>Kategori</b>
                        <b-form-select v-model="categorySelected">
                            <b-select-option v-for="(category, index) in categories" :key="index" :value="category.id">{{ category.name }}</b-select-option>
                        </b-form-select>
                </div>
                </div>
                <b-button v-if="rediger" class="action-buttons" variant="success" @click="redigerOpgave">Godkend</b-button>
                <b-button class="action-buttons" v-if="isTaskCreator && !rediger" @click="edit" variant="primary">Rediger</b-button>
                <b-button variant=primary v-if="rediger" @click="edit">Tilbage</b-button>
                <div class="d-flex" style="flex-direction: column">
                    <h1 class="mt-4">Kommentarer</h1>
                    <b-textarea class="comment" v-model="comment" placeholder="Skriv her.." rows="4"></b-textarea>
                    <button @click="kommenter()" class="btn btn-primary mt-3 ml-auto">Skriv kommentar</button>
                    <div class="comments" v-if="allowLoad">
                        <div :key="comment.date" v-for="comment in comments" class="comment" :class="{ active : comment.userID === task.userId }">
                        <div class="d-flex"><strong>{{ comment.firstName }} {{ comment.lastName }}</strong><p class="ml-auto">{{ mom(new Date(comment.date)).format("DD-MM-YYYY") }} kl. {{ mom(new Date(comment.date)).add(1, "hours").format("HH:mm") }}</p></div>
                        <p>{{ comment.message }}</p>
                    </div>
                    </div>
                    <div v-else class="no-comments">
                        Der er ingen kommentarer på opslaget.
                    </div>
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
                        </svg>&nbsp;
                        <a class="name" @click="seeProfile($store.state.userID)">{{ firstName }} {{ lastName }}</a>
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
                    <p><strong >Status: </strong><span :class="status" style="margin-right: 5px;">{{status}}</span>
                    <b-button class="circleButton" title="Skift til ledig" style="background-color: #28a745;" @click="changeGreen()" v-if="isTaskCreator && (!isGreen || isYellow)"></b-button>
                    <b-button class="circleButton" title="Skift til løst" style="background-color: #dc3545;" @click="changeRed()"  v-if="isTaskCreator && (isGreen || isYellow)"></b-button>
                    <b-modal v-model="modalShow" hide-footer title="Giv en bedømmelse">
                        <h3>{{ rateUser.firstName }} {{ rateUser.lastName }}</h3>
                        <b-form-rating class="rating" no-border inline v-model="rating"></b-form-rating>
                        <p style="margin-bottom: 1px;">Besked:</p>
                        <b-textarea v-model="message" rows="4"></b-textarea>
                        <div style="text-align: right;">
                            <b-button variant="primary" style="margin-top: 10px" @click="RateUser()">Bedøm</b-button>
                        </div>
                    </b-modal>
                    <p v-if="isTaskCreator"><strong>Visninger: </strong>{{ task.pageViews }}</p>
                </div>
                 <b-button variant="primary" class="tilmeldt-button mt-3" to="/assignedUsers" v-if="isTaskCreator">Tilmeldte brugere</b-button>
            </b-col>
        </b-row>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { GetTaskById, RedigerTask } from "../api/task";
import { GetBrugerById, GetLoggedInId } from "../api/user";
import { GetAssignedUserMatch, GetAssignedUsersOnMyTask, OpretAssignedUser, DeleteAssignedUser } from "../api/assignedUser";
import { Task } from "../types/task";
import { User } from "../types/user";
import { AssignedUser } from "../types/assignedUser";
import { OpretComment, GetCommentsForTask } from "../api/comments";
import { GetCategories } from "../api/category";
import { CreateRating } from "../api/rating";
import Comment from "../types/comments";
import moment from "moment";

@Component
export default class SeeMore extends Vue {
    task = {} as Task;
    date: Date = new Date();
    title = "";
    price = "";
    description = "";
    region = "";
    status = "";
    isGreen = false;
    isYellow = false;

    user = {} as User;
    firstName = "";
    lastName = "";
    userMail = "";
    userNumber = "";

    assignedUser = {} as AssignedUser;
    signedUp = false;
    isTaskCreator = false;
    rediger = false;
    titleInput = "";
    opgaveBeskrivelseInput = "";
    prisInput = "";
    categories = [];
    categorySelected = 0;

    modalShow = false;
    notChosen = false;
    rateUser = {} as User;
    rating = 0;
    message = "";

    // Comments
    comment = "";
    comments = [] as Comment[];
    allowLoad = false;

    // Methods

    mom(date: Date) {
        return moment(date);
    }

    changeGreen() {
        RedigerTask(this.$store.state.taskID, this.$store.state.userID, this.task.categoryId, this.task.date.toString(), this.task.title, this.task.price, this.task.description, "Ledig", this.task.promoted, this.task.region, this.task.promotedEnd.toString(), (this.task.pageViews));
        this.status = "Ledig";
        this.isGreen = true;
        this.isYellow = false;
    }

    changeRed() {
        if (this.rateUser.id) {
            this.modalShow = true;
            this.rating = 0;
            this.message = "";
        } else {
            this.notChosen = true;
        }
    }

    setValues() {
        RedigerTask(this.$store.state.taskID, this.$store.state.userID, this.task.categoryId, this.task.date.toString(), this.task.title, this.task.price, this.task.description, "Løst", this.task.promoted, this.task.region, this.task.promotedEnd.toString(), (this.task.pageViews));
        this.status = "Løst";
        this.isGreen = false;
        this.isYellow = false;
    }

    edit() {
        this.rediger = !this.rediger;
    }

    redigerOpgave() {
        RedigerTask(this.$store.state.taskID, this.$store.state.userID, this.categorySelected, this.task.date.toString(), this.titleInput, Number(this.prisInput), this.opgaveBeskrivelseInput, this.task.status, this.task.promoted, this.task.region, this.task.promotedEnd.toString(), this.task.pageViews).then(response => {
            this.rediger = false;
            this.getAllElements();
        });
    }

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
                this.titleInput = this.task.title;
                this.opgaveBeskrivelseInput = this.task.description;
                this.prisInput = this.task.price.toString();

                this.loadKommentarer();

                GetBrugerById(this.task.userId).then(response => {
                    this.firstName = response.data.firstName;
                    this.lastName = response.data.lastName;
                    this.userMail = response.data.email;
                    this.userNumber = response.data.phone;
                });
                if (this.status === "Ledig") {
                    this.isGreen = true;
                }
                if (this.status === "Aktiv") {
                    this.isYellow = true;
                }

                RedigerTask(this.$store.state.taskID, this.$store.state.userID, this.task.categoryId, this.task.date.toString(), this.task.title, this.task.price, this.task.description, this.task.status, this.task.promoted, this.task.region, this.task.promotedEnd.toString(), (this.task.pageViews + 1));
                this.task.pageViews++;
            });

            GetCategories().then(response => {
                this.categories = response.data;
                this.categorySelected = response.data[0].id;
                console.log(this.categorySelected);
            }).catch(() => {
                console.log("Error");
            });

            if (this.$store.state.loggedIn) {
                if (GetLoggedInId() === this.$store.state.userID.toString()) {
                    this.isTaskCreator = true;
                } else {
                    this.setup();
                }
            }

            GetAssignedUsersOnMyTask(this.$store.state.taskID).then(response => {
                let assignedUsers = Array<AssignedUser>();
                assignedUsers = response.data;
                assignedUsers.forEach(user => {
                    if (user.accepted) {
                        GetBrugerById(user.userID).then(response => {
                            this.rateUser = response.data;
                        });
                    }
                });
            });
        }
    }

    getAllElements() {
        GetTaskById(this.$store.state.taskID).then(response => {
            this.task = response.data;
            this.region = response.data.region;
            this.status = response.data.status;
            this.date = response.data.date;
            this.title = response.data.title;
            this.price = response.data.price;
            this.description = response.data.description;
            this.titleInput = this.task.title;
            this.opgaveBeskrivelseInput = this.task.description;
            this.prisInput = this.task.price.toString();

            this.loadKommentarer();

            GetBrugerById(this.task.userId).then(response => {
                this.firstName = response.data.firstName;
                this.lastName = response.data.lastName;
                this.userMail = response.data.email;
                this.userNumber = response.data.phone;
            });
            if (this.status === "Ledig") {
                this.isGreen = true;
            }
            if (this.status === "Aktiv") {
                this.isYellow = true;
            }
        });
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

    seeProfile(id: number) {
        this.$router.push({ name: "Profiles", query: { user: id.toString() } });
    }

    loadKommentarer() {
        this.allowLoad = false;
        GetCommentsForTask(this.task.id).then(response => {
            this.comments = response.data.reverse();
            this.comment = "";

            let count = 0;

            this.comments.forEach(element => {
                GetBrugerById(element.userID).then(response => {
                    count++;
                    element.firstName = response.data.firstName;
                    element.lastName = response.data.lastName;

                    if (count === this.comments.length) this.allowLoad = true;
                });
            });
        });
    }

    kommenter() {
        if (this.comment === "") return;
        OpretComment(this.task.id, GetLoggedInId(), new Date(), this.comment).then(() => {
            this.loadKommentarer();
        });
    }

    RateUser() {
        CreateRating(this.rateUser.id, this.$store.state.taskID, new Date(), this.rating, this.message);
        this.modalShow = false;
        this.rating = 0;
        this.message = "";
    }
}
</script>

<style lang="scss" scoped>
@import "@/assets/main.scss";

.comments {
    margin-top: 35px;

    .comment {
        background: $light;
        margin-bottom: 10px;
        padding: 12px;
        border-radius: 6px;
        border-left: 4px solid #555;

        &.active{
            border-left: 4px solid $primary;
        }
    }
}

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

    p {
        overflow-wrap: break-word;
    }
}

.action-buttons {
    margin: 7px;
}

.name {
    color: $gray;
    cursor: pointer;
}

.tilmeldt-button {
    width: 100%;
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

.pris {
    background: $primary;
    font-weight: 700;
    padding: 5px;
    padding-left: 10px;
    color: white;
    border-radius: 0 7px 0 7px;
    display: inline-block;
    position: absolute;
    top: 0px;
    right: 0px;
    text-align: center;
    width: 12%;
    animation:blinking 1.5s infinite;
}

@keyframes blinking{
0%{   color: red;   }
}

.circleButton {
    padding: 0px;
    display: inline-block;
    width: 13px;
    height: 13px;
    border-radius: 15px;
    margin-right: 3px;
    border-style: none;
}

.title {
    width: 70%;
    margin-bottom: 10px;
}

.circleButton:hover {
    border-style: solid;
    border-color: black;
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

.rating {
    padding: 0;
    background: none;
    border: none;
}
.rating:focus {
    outline: none;
    box-shadow: none;
}
</style>
