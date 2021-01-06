<template>
  <div class="home">
    <div class="container">
        <b-row>
        <b-col lg="6" sm=12>
            <h1 class="title">Workio</h1>
            <p class="sub">Din markedsplads til arbejdsopgaver.</p>
            <p class="desc">Om du ønsker at få løst arbejdsopgaver, eller være personen der løser opgaverne, er Workio stedet for dig. Hos Workio sikrer vi os at processen er så nem for begge parter som muligt, hvilket ultimativt giver den bedste arbejdsoplevelse.</p>
        </b-col>
        <b-col lg="6" sm=12>
            <img src="../assets/forside.jpg">
        </b-col>
    </b-row>
    <h2>Promoverede opgaver</h2>
    <div class="overview">
        <div @click="seeMore(task.id,task.userId, task.id)" class="task" v-for="(task, idx) in tasks" :key="idx">
            <Tile :title="task.title" :price="task.price" :description="task.description" :categoryId="task.categoryId" :region="task.region" :promoted="task.promoted"/>
        </div>
    </div>

    <h2>Nye arbejdsopgaver</h2>
    <div class="overview">
        <div @click="seeMore(task.id,task.userId, task.id)" class="task" v-for="(task, idx) in newTasks" :key="idx">
            <Tile :title="task.title" :price="task.price" :description="task.description" :categoryId="task.categoryId" :region="task.region" :promoted="task.promoted"/>
        </div>
    </div>
    <div class="cta">
        <h1>Er du klar til at deltage?</h1>
        <p>Opret en arbejdsopgave og kom i gang.</p>
        <b-btn to="/arbejdsopgave" size="lg" variant="light">Opret arbejdsopgave</b-btn>
    </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import Tile from "@/components/Tile.vue";
import { GetTasks } from "../api/task";

// Types
import { Task } from "../types/task";

@Component({
    components: {
        Tile
    }
})
export default class Home extends Vue {
    tasks = Array<Task>();
    newTasks = Array<Task>();

    mounted() {
        GetTasks().then(response => {
            response.data = response.data.reverse();
            this.newTasks = response.data.filter((t: Task) => t.promoted === false && t.status !== "Aktiv" && t.status !== "Løst").slice(0, 4);
            this.tasks = response.data.filter((t: Task) => t.promoted && t.status !== "Aktiv" && t.status !== "Løst").slice(0, 4);
        });
    }

    seeMore(taskID: number, userID: number, assignedTaskID: number) {
        this.$store.commit("userID", userID);
        this.$store.commit("AssignedTaskID", assignedTaskID);
        this.$store.commit("taskID", taskID);
        this.$router.push({ name: "SeeMore" });
    }
}
</script>

<style scoped lang="scss">
@import "../assets/main.scss";

    .title {
        font-size: 100px;
    }
    .sub {
        font-size: 26px;
        color: #777;
    }
    .desc {
        color: #555;
        max-width: 650px;
        font-size: 18px;
    }
    img {
        border-radius: 6px;
        width: 100%;
    }
    .container {
        max-width: 1300px;
    }
    h2 {
        color: #212121;
        margin-top: 65px;
        margin-bottom: 15px;
        font-size: 24px;
    }

    .cta {
        margin-top: 50px;
        text-align: center;
        background: $primary;
        color: #fff;
        padding: 80px 15px;
        border-radius: 6px;

        h1, p {
            color: #fff;
        }
        h1 {
            font-size: 50px;
        }
        p {
            font-size: 19px;
        }
    }
</style>
