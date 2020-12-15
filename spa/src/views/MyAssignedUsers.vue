<template>
    <div v-if="AssignedUsers.length < 1"><h3 class="nousers"><span>Ingen tilmeldte brugere!</span></h3></div>
    <div v-else>
        <h2>Tilmeldte brugere</h2>
            <div v-for="(assignedUser, idx) in AssignedUsers" :key="idx" class="box">
                <div>
                    <div>
                        <h4>Navn: <b-button @click="goToProfile(getUser.id)" class="goto">{{ getUser[idx].firstName + " " + getUser[idx].lastName }}</b-button></h4>
                        <h4 v-if="!assignedUser.accepted" class="notaccepted">Afventer</h4>
                        <h4 v-if="assignedUser.accepted" class="accepted">Accepteret</h4>
                    </div>
                    <b-button v-if="!assignedUser.accepted" variant="success" class="buttons" @click="AcceptUser(assignedUser.id, assignedUser.taskID, assignedUser.userID, true)">Accepter</b-button>
                    <b-button variant="danger" class="buttons" @click="DelAssignedUser(assignedUser.id)">Afslag</b-button>
                </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { GetAssignedUsersOnMyTask, DeleteAssignedUser, UpdateAssignedUser, RemoveUsersWhenAccepted } from "@/api/assignedUser";
import { GetBrugerById } from "@/api/user";
import { AssignedUser } from "../types/assignedUser";
import { User } from "../types/user";

@Component
export default class MyTasks extends Vue {
    AssignedUsers = Array<AssignedUser>();
    getUser = Array<User>();
    status = {} as AssignedUser;

    mounted() {
        GetAssignedUsersOnMyTask(this.$store.state.AssignedTaskID).then(response => {
            this.AssignedUsers = response.data;
            this.AssignedUsers.forEach(element => {
                GetBrugerById(element.userID).then(response => {
                    this.getUser.push(response.data);
                    console.log(response.data.id);
                });
            });
        }).catch((err) => {
            console.error(err);
        });
    }

    DelAssignedUser(id: number) {
        if (confirm("Er du sikker?")) {
            DeleteAssignedUser(id).then(response => {
                console.log("User removed: " + response.status);
            }).catch(() => {
                console.log("Couldn't remove user");
            });
        }
    }

    goToProfile(id: any) {
        this.$router.push({ name: "Profile", query: { user: id } });
    }

    AcceptUser(id: number, taskID: number, userID: number, accepted: boolean) {
        if (confirm("Er du sikker?")) {
            UpdateAssignedUser(id, taskID, userID, accepted).then(response => {
                console.log("Bruger opdateret: " + response.status);
            }).then(() => {
                RemoveUsersWhenAccepted(this.$store.state.AssignedTaskID).then(response => {
                    this.$forceUpdate();
                    console.log("All other users where removed: " + response.status);
                });
            }).catch((err) => {
                console.error(err);
            });
        }
    }
}
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";
.box div div{
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    width: 100%;
    background: $light;
    padding: 20px;
    border-radius: 10px;
    height: 80px;
    margin-bottom: 5px;
}
.buttons {
    max-height: 40px;
    margin: 5px;
}
.notaccepted {
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
    background: $primary;
    color: white;
    border-radius: 7px;
}
</style>
