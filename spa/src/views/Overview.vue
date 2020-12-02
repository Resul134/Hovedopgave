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
import { Component, Vue, Watch } from "vue-property-decorator";
import { GetTasks, GetTasksByCategory, GetTasksByFilter } from "../api/task";
import { Task } from "../types/task";
import Tile from "@/components/Tile.vue";

@Component({
    components: {
        Tile
    }
})
export default class Overview extends Vue {
    chosenCategory = Number(this.$route.params.categoryId);
    tasks = Array<Task>();

    checkCategory() {
        this.tasks = Array<Task>();
        if (this.chosenCategory === 0) {
            this.GetAllTasks();
        } else {
            this.GetAllTasksByCategory(this.chosenCategory);
        }
    }

    GetFilterMatchingTasks(categoryId: string, region: string, minPrice: string, maxPrice: string, minDate: string, maxDate: string) {
        GetTasksByFilter(categoryId, region, minPrice, maxPrice, minDate, maxDate).then(response => {
            if (response.status === 200) {
                this.tasks = response.data;
            }
        }).catch(() => {
            console.log("Error getting tasks");
        });
    }

    GetAllTasks() {
        GetTasks().then(response => {
            if (response.status === 200) {
                this.tasks = response.data;
            }
        }).catch(() => {
            console.log("Error getting tasks");
        });
    }

    GetAllTasksByCategory(id: number) {
        GetTasksByCategory(id).then(response => {
            if (response.status === 200) {
                this.tasks = response.data;
            }
        }).catch(() => {
            console.log("Error getting tasks by category");
        });
    }

    mounted() {
        this.checkCategory();
    }

    @Watch("$route.params", { immediate: true, deep: true })
    onUrlChange() {
        const params = this.$route.params;
        this.GetFilterMatchingTasks(params.categoryId, params.region, params.minPrice, params.maxPrice, params.minDate, params.maxDate);
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
