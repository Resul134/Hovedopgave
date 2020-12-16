<template>
    <div>
        <div v-if="!tasks.length">
            Nothing here
        </div>
        <div class="overview" v-else>
            <div @click="seeMore(task.id,task.userId)" class="task" v-for="(task, idx) in tasks" :key="idx">
                <Tile :title="task.title" :price="task.price" :description="task.description" :categoryId="task.categoryId" :region="task.region" :promoted="task.promoted"/>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { GetTasksByFilter } from "../api/task";
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

    seeMore(taskID: number, userID: number) {
        this.$store.commit("userID", userID);
        this.$store.commit("taskID", taskID);
        this.$router.push({ name: "SeeMore" });
    }

    GetFilterMatchingTasks(categoryId: string, region: string, minPrice: string, maxPrice: string, minDate: string, maxDate: string, search: string) {
        this.tasks = Array<Task>();
        GetTasksByFilter(categoryId, region, minPrice, maxPrice, minDate, maxDate, search).then(response => {
            if (response.status === 200) {
                this.tasks = response.data;
            }
        }).catch(() => {
            console.log("Error getting tasks");
        });
    }

    @Watch("$route.query", { immediate: true, deep: true })
    onUrlChange() {
        const categoryId = this.$route.query.categoryId !== undefined ? this.$route.query.categoryId : "";
        const region = this.$route.query.region !== undefined ? this.$route.query.region : "";
        const minPrice = this.$route.query.minPrice !== undefined ? this.$route.query.minPrice : "";
        const maxPrice = this.$route.query.maxPrice !== undefined ? this.$route.query.maxPrice : "";
        const minDate = this.$route.query.minDate !== undefined ? this.$route.query.minDate : "";
        const maxDate = this.$route.query.maxDate !== undefined ? this.$route.query.maxDate : "";
        const search = this.$route.query.search !== undefined ? this.$route.query.search : "";
        this.GetFilterMatchingTasks(categoryId.toString(), region.toString(), minPrice.toString(), maxPrice.toString(), minDate.toString(), maxDate.toString(), search.toString());
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
    overflow-wrap: break-word;
}

.task:nth-child(4n) {
    margin-right: 0;
}
</style>
