<template>
    <div>
        <h1>Mine arbejdsopgaver</h1>
        <div v-if="!tasks.length" class="tilmeld-error">
            Du har ikke oprettet nogle arbejdsopgaver!
        </div>
        <div class="overview" v-else>
            <div @click="AssignedUsers(task.id, task.userId, task.id)" class="task" v-for="(task, idx) in tasks" :key="idx">
                <Tile :title="task.title" :categoryId="task.categoryId" :region="task.region" :description="task.description" :price="task.price" :promoted="task.promoted" />
            </div>
        </div>
    </div>
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import Tile from "@/components/Tile.vue";
import { Task } from "../types/task";
import { GetTasksByUserID } from "../api/task";
import { GetLoggedInId } from "@/api/user";

@Component({
    components: {
        Tile
    }
})
export default class MyTasks extends Vue {
    tasks = Array<Task>();

    AssignedUsers(AssignedtaskID: number, userid: number, taskid: number) {
        this.$store.commit("AssignedTaskID", AssignedtaskID);
        this.$store.commit("userID", userid);
        this.$store.commit("taskID", taskid);
        this.$router.push({ name: "SeeMore" });
    }

    mounted() {
        GetTasksByUserID(GetLoggedInId()).then(response => {
            this.tasks = response.data.reverse().sort((x: any, y: any) => y.promoted - x.promoted);
        }).catch((err) => {
            console.error(err);
        });
    }
}
</script>
