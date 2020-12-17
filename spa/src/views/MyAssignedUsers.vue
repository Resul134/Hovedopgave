<template>
    <div v-if="AssignedUsers.length < 1"><h3 class="nousers"><span>Ingen tilmeldte brugere!</span></h3></div>
    <div v-else>
        <h2>Tilmeldte brugere</h2>
            <div v-for="(assignedUser, idx) in AssignedUsers" :key="idx" class="box">
                <div>
                    <div>
                        <h4>Navn: <b-button @click="goToProfile(assignedUser.userID)" class="goto">{{ getUser[idx].firstName + " " + getUser[idx].lastName }}</b-button></h4>
                        <h4 v-if="!assignedUser.accepted" class="notaccepted">Ikke godkendt</h4>
                        <h4 v-if="assignedUser.accepted" class="accepted">Godkendt</h4>
                    </div>
                    <b-button v-if="!assignedUser.accepted" variant="success" class="buttons" @click="AcceptUser(assignedUser.id, assignedUser.taskID, assignedUser.userID, true)">Accepter</b-button>
                    <b-button v-if="assignedUser.accepted" variant="danger" class="buttons" @click="RegretAssignedUser(assignedUser.id, assignedUser.taskID, assignedUser.userID, false)">Fortryd</b-button>
                </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { GetAssignedUsersOnMyTask, UpdateAssignedUser } from "@/api/assignedUser";
import { GetBrugerById } from "@/api/user";
import { AssignedUser } from "../types/assignedUser";
import { User } from "../types/user";

@Component
export default class MyTasks extends Vue {
    AssignedUsers = Array<AssignedUser>();
    getUser = Array<User>();
    status = {} as AssignedUser;

    mounted() {
        this.getAllUsers();
    }

    getAllUsers() {
        GetAssignedUsersOnMyTask(this.$store.state.AssignedTaskID).then(response => {
            this.AssignedUsers = response.data;
            this.AssignedUsers.forEach(element => {
                console.log(this.AssignedUsers);
                GetBrugerById(element.userID).then(response => {
                    this.getUser.push(response.data);
                    console.log(response.data.id);
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

    RegretAssignedUser(id: number, taskID: number, userID: number, accepted: boolean) {
        if (confirm("Er du sikker?")) {
            UpdateAssignedUser(id, taskID, userID, accepted).then(response => {
                console.log("Fortryd bruger" + response.status);
                this.getAllUsers();
            }).catch(() => {
                console.log("Couldn't remove user");
            });
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
    width: 170px;
    text-align: center;
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
    background: #28a745;
    color: white;
    border-radius: 7px;
    width: 170px;
    text-align: center;
}
</style>
