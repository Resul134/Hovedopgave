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
import { Query } from "../types/query";
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
        console.log(taskID);
        console.log(userID);
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
        let queries = Array<Query>();
        let categoryId = "";
        let region = "";
        let minPrice = "";
        let maxPrice = "";
        let minDate = "";
        let maxDate = "";
        let search = "";
        queries = JSON.parse(String(this.$route.query.payload));
        queries.forEach(element => {
            switch (element.name) {
            case "categoryId":
                categoryId = element.value;
                break;
            case "region":
                region = element.value;
                break;
            case "minPrice":
                minPrice = element.value;
                break;
            case "maxPrice":
                maxPrice = element.value;
                break;
            case "minDate":
                minDate = element.value;
                break;
            case "maxDate":
                maxDate = element.value;
                break;
            case "search":
                search = element.value;
                break;
            }
        });
        this.GetFilterMatchingTasks(categoryId, region, minPrice.toString(), maxPrice.toString(), minDate, maxDate, search);
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
