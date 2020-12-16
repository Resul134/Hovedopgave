<template>
    <div>
        <h1>Mine arbejdsopgaver</h1>
        <div v-if="!tasks.length">
            Du har ikke oprettet nogle arbejdsopgaver!
        </div>
        <div class="overview" v-else>
            <div @click="AssignedUsers(task.id)" class="task" v-for="(task, idx) in tasks" :key="idx">
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

    AssignedUsers(AssignedtaskID: number) {
        // this.$store.commit("AssignedUserID", AssignedUserID);
        this.$store.commit("AssignedTaskID", AssignedtaskID);
        this.$router.push({ name: "AssignedUsers" });
    }

    mounted() {
        GetTasksByUserID(GetLoggedInId()).then(response => {
            this.tasks = response.data.sort((x: any, y: any) => y.promoted - x.promoted);
        }).catch((err) => {
            console.error(err);
        });
    }
}
</script>
<style lang="scss" scoped>
@import "../assets/main.scss";
.overview {
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
</style>
