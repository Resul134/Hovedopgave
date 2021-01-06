<template>
    <div>
        <h2 class="accepted">Godkendt</h2>
        <p v-if="TasksAccepted.length < 1">Du har ingen godkendte tilmeldinger.</p>
        <div>
            <div v-for="(acceptedTask, idx) in TasksAccepted" :key="idx" class="box">
                <div @click="goToSeeMore(acceptedTask.id)" class="task">
                    <Tile :title="acceptedTask.title" :categoryId="acceptedTask.categoryId" :region="acceptedTask.region" :description="acceptedTask.description" :price="acceptedTask.price" :promoted="acceptedTask.promoted" />
                </div>
            </div>
        </div>
        <h2 class="pending">Afventer afklaring</h2>
        <p v-if="TasksPending.length < 1 || pendingTaskEmpty">Du har ingen afventende tilmeldinger.</p>
        <div>
            <div v-for="(pendingTask, idx) in TasksPending" :key="idx" class="box">
                <div @click="goToSeeMore(pendingTask.id)" class="task" v-if="pendingTask.status === 'Ledig'">
                    <Tile :title="pendingTask.title" :categoryId="pendingTask.categoryId" :region="pendingTask.region" :description="pendingTask.description" :price="pendingTask.price" :promoted="pendingTask.promoted" />
                </div>
            </div>
        </div>
        <h2 class="denied">Afvist</h2>
        <p v-if="TasksDenied.length < 1 || denyTaskEmpty">Du har ingen afviste tilmeldinger.</p>
        <div>
            <div v-for="(deniedTask, idx) in TasksDenied" :key="idx" class="task">
                <div @click="goToSeeMore(deniedTask.id)" v-if="deniedTask.status === 'Aktiv' || deniedTask.status === 'Løst'">
                    <Tile :title="deniedTask.title" :categoryId="deniedTask.categoryId" :region="deniedTask.region" :description="deniedTask.description" :price="deniedTask.price" :promoted="deniedTask.promoted" />
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { GetMyAcceptedAssignments, GetMyDeniedAssignments } from "@/api/assignedUser";
import { GetTaskById } from "@/api/task";
import Tile from "@/components/Tile.vue";
import { GetLoggedInId } from "@/api/user";
import { AssignedUser } from "../types/assignedUser";
import { Task } from "../types/task";

@Component({
    components: {
        Tile
    }
})
export default class MyTasks extends Vue {
    status = {} as AssignedUser;
    acceptedTasks = Array<AssignedUser>();
    deniedTasks = Array<AssignedUser>();
    pendingTasks = Array<AssignedUser>();

    TasksAccepted = Array<Task>();
    TasksDenied = Array<Task>();
    TasksPending = Array<Task>();
    denyTaskEmpty = true;
    pendingTaskEmpty = true;

    mounted() {
        this.AcceptedAssignment();
        this.PendingAssigment();
        this.DeniedAssigment();
    }

    AcceptedAssignment() {
        GetMyAcceptedAssignments(GetLoggedInId()).then(response => {
            this.acceptedTasks = response.data;
            this.acceptedTasks.forEach(element => {
                GetTaskById(element.taskID).then(response => {
                    this.TasksAccepted.push(response.data);
                });
            });
        });
    }

    PendingAssigment() {
        GetMyDeniedAssignments(GetLoggedInId()).then(response => {
            this.pendingTasks = response.data;
            this.pendingTasks.forEach(element => {
                GetTaskById(element.taskID).then(response => {
                    this.TasksPending.push(response.data);

                    this.TasksPending.forEach(e => {
                        console.log(e);
                        if (e.status === "Ledig") {
                            this.pendingTaskEmpty = false;
                        }
                    });
                }).catch((err) => {
                    console.error(err);
                });
            });
        });
    }

    DeniedAssigment() {
        GetMyDeniedAssignments(GetLoggedInId()).then(response => {
            this.deniedTasks = response.data;
            this.deniedTasks.forEach(element => {
                GetTaskById(element.taskID).then(response => {
                    this.TasksDenied.push(response.data);

                    this.TasksDenied.forEach(e => {
                        if (e.status === "Aktiv" || e.status === "Løst") {
                            this.denyTaskEmpty = false;
                        }
                    });
                }).catch((err) => {
                    console.error(err);
                });
            });
        });
    }

    goToSeeMore(taskID: number) {
        this.$store.commit("taskID", taskID);
        this.$router.push({ name: "SeeMore" });
        console.log("TaskID:" + taskID);
    }
}
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";
.accepted {
    background: #28a745;
    color: white;
    border-radius: 7px;
    text-align: center;
    width: 180px;

}
.box {
    display: flex;
    flex-wrap: wrap;
}

.task {
    width: 24.25%;
    margin-right: 1%;
    margin-bottom: 1%;
}

.task:nth-child(4n) {
    margin-right: 0;
}

.denied {
    background: crimson;
    border-radius: 7px;
    color: white;
    text-align: center;
    width: 130px;
}

.pending {
    background: orange;
    border-radius: 7px;
    color: white;
    width: 280px;
    text-align: center;
}

</style>
