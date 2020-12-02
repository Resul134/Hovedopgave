<template>
    <div>
        <div v-if="!tasks.length">
            Nothing here
        </div>
        <div class="overview" v-else>
            <div class="task" v-for="(task, idx) in tasks" :key="idx">
                <Tile :title="task.title" :price="task.price" :description="task.description" :categoryId="task.categoryId" :region="task.region" :promoted="task.promoted"/>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { GetTasks } from "../api/task";
import { Task } from "../types/task";
import Tile from "@/components/Tile.vue";

@Component({
    components: {
        Tile
    }
})
export default class Overview extends Vue {
    tasks = Array<Task>();

    mounted() {
        GetTasks().then(response => {
            if (response.status === 200) {
                this.tasks = response.data;
            }
        }).catch(() => {
            console.log("Error getting tasks");
        });
    }
}
</script>
<style scoped lang="scss">
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
