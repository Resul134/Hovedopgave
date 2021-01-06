<template>
    <div>
        <div v-if="!allow"><h3 class="nousers"><span>Ingen tilmeldte brugere!</span></h3></div>
        <div v-if="AssignedUsers.length > 0 && allow">
            <h2>Tilmeldte brugere</h2>
            <div v-for="(assignedUser, idx) in AssignedUsers" :key="idx" class="box">
                <div>
                    <div class="background">
                        <div class="flex">
                            <h4>Navn: <b-button @click="goToProfile(assignedUser.userID)" class="goto">{{ getUser[idx].firstName + " " + getUser[idx].lastName }}</b-button></h4>
                            <h4 v-if="!assignedUser.accepted" class="notaccepted">Ikke godkendt</h4>
                            <h4 v-if="assignedUser.accepted" class="accepted">Godkendt</h4>
                        </div>
                    </div>
                    <b-button v-if="!assignedUser.accepted" variant="success" class="buttons" @click="AcceptUser(assignedUser.id, assignedUser.taskID, assignedUser.userID, true)">Accepter</b-button>
                    <b-button v-if="assignedUser.accepted" variant="danger" class="buttons" @click="RegretAssignedUser(assignedUser.id, assignedUser.taskID, assignedUser.userID, false)">Fortryd</b-button>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { GetAssignedUsersOnMyTask, UpdateAssignedUser } from "@/api/assignedUser";
import { GetBrugerById } from "@/api/user";
import { GetTaskById, RedigerTask } from "@/api/task";
import { AssignedUser } from "../types/assignedUser";
import { User } from "../types/user";
import { Task } from "@/types/task";

@Component
export default class MyTasks extends Vue {
    AssignedUsers = Array<AssignedUser>();
    getUser = Array<User>();
    status = {} as AssignedUser;
    task = {} as Task
    allow = false;

    mounted() {
        this.getAllUsers();
        this.getTask();
    }

    getAllUsers() {
        GetAssignedUsersOnMyTask(this.$store.state.AssignedTaskID).then(response => {
            this.AssignedUsers = response.data;
            let lidx = 0;
            this.AssignedUsers.forEach(element => {
                GetBrugerById(element.userID).then(response => {
                    this.getUser.push(response.data);

                    lidx++;
                    if (lidx === this.AssignedUsers.length) {
                        this.allow = true;
                    }
                });
                if (element.accepted) {
                    console.log(element.accepted);
                    this.AssignedUsers = this.AssignedUsers.filter((obj: AssignedUser) => {
                        return obj.accepted;
                    });
                }
            });
        }).catch((err) => {
            console.error(err);
        });
    }

    getTask() {
        GetTaskById(this.$store.state.AssignedTaskID).then(response => {
            this.task = response.data;
        });
    }

    RegretAssignedUser(id: number, taskID: number, userID: number, accepted: boolean) {
        if (confirm("Er du sikker?")) {
            UpdateAssignedUser(id, taskID, userID, accepted).then(response => {
                console.log("Fortryd bruger");
                this.getAllUsers();
            }).catch(() => {
                console.log("Couldn't remove user");
            });
            RedigerTask(this.task.id, this.task.userId, this.task.categoryId, this.task.date.toString(), this.task.title, this.task.price, this.task.description, "Ledig", this.task.promoted, this.task.region, this.task.promotedEnd.toString(), this.task.pageViews);
        }
    }

    goToProfile(id: any) {
        this.$router.push({ name: "Profiles", query: { user: id.toString() } });
    }

    AcceptUser(id: number, taskID: number, userID: number, accepted: boolean) {
        if (confirm("Er du sikker?")) {
            UpdateAssignedUser(id, taskID, userID, accepted).then(response => {
                console.log("Bruger opdateret: " + response.status);
                this.getAllUsers();
            }).catch((err) => {
                console.error(err);
            });
            RedigerTask(this.task.id, this.task.userId, this.task.categoryId, this.task.date.toString(), this.task.title, this.task.price, this.task.description, "Aktiv", this.task.promoted, this.task.region, this.task.promotedEnd.toString(), this.task.pageViews);
        }
    }
}
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";
.background {
    width: 100%;
    background: $light;
    padding: 20px;
    border-radius: 10px;
    margin-bottom: 5px;
}
.flex {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
}
.buttons {
    max-height: 40px;
    margin: 5px;
}
.notaccepted {
    padding: 5px;
    background: crimson;
    border-radius: 7px;
    color: white;
}
.goto{
    margin-top: -7px;
    font-size: 1.5rem;
    background: none;
    border: none;
    color: $primary;
    outline: none;
}
.nousers{
    text-align: center;
}
.nousers span {
    border-radius: 7px;
}
.accepted {
    padding: 5px;
    background: #28a745;
    color: white;
    border-radius: 7px;
}
</style>
