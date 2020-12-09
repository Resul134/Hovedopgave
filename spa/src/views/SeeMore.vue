<template>
 <div>
          <b-row>
              <b-col>
                  <div class="seeMore">
                      <div class="tag">
                      <p >{{ price }} kr</p>
                    </div>
                      <h1 style="text-align:center;">{{title}}</h1>
                    <div class="content-title">
                      <p>{{description}} </p>
                  </div>
                  </div>
                  <h1 style="text-align:center;">Kommentarer</h1>
                    <div class="comments">
                  </div>
                  <button class="buttonLeft">Skriv kommentar</button>
              </b-col>
              <b-col cols="3">
                  <button class="buttonFill">Tilmeld</button>
                  <div class="box">
                      <p class="textStyle">Profil Info<p/>
                      <p>{{firstName}}</p>
                      <p>{{lastName}}</p>
                      <p>{{userMail}}</p>
                      <p>{{userNumber}}</p>
                  </div>
                  <p>{{ date | formatDate }}</p>
                  <p>{{region}}</p>
                  <p>{{status}}</p>
              </b-col>
          </b-row>
      </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { GetTaskById } from "../api/task";
import { GetBrugerById } from "../api/user";
import { Task } from "../types/task";
import { User } from "../types/user";

@Component
export default class SeeMore extends Vue {
    task = {} as Task;
    date: Date = new Date();
    title = "";
    price = "";
    description = "";
    region = "";
    status = "";
    userID = 3;

    user = {} as User;
    firstName = "";
    lastName = "";
    userMail = "";
    userNumber = "";

    mounted() {
        if (this.$store.state.taskID === null) {
            this.$router.push({ name: "About" });
        } else {
            GetTaskById(this.$store.state.taskID).then(response => {
                this.task = response.data;
                this.region = response.data.region;
                this.status = response.data.status;
                this.date = response.data.date;
                this.title = response.data.title;
                this.price = response.data.price;
                this.description = response.data.description;
            });
            GetBrugerById(this.$store.state.userID).then(response => {
                this.firstName = response.data.firstName;
                this.lastName = response.data.lastName;
                this.userMail = response.data.email;
                this.userNumber = response.data.phone;
            });
        }
    }
}
</script>

<style lang="scss" scoped>
@import "@/assets/main.scss";

.content-title{
    display: flex;
    justify-content: space-between;
    margin-bottom: 10px;
}

.seeMore{
    border-radius: 7px;
    padding: 7px;
    border-style: solid;
    border-width: 1px;
}
.tag p {
    background: $primary;
    font-weight: 700;
    color: white;
    border-radius: 0 5px 0 5px;
    width: 100px;
    margin-left: auto;
    margin-right: 0;
}
.box {
    border-style: ridge;
    padding: 7px;
    border-radius: 7px;
    height: 220px;
}
.buttonFill {
    width: -webkit-fill-available;
    background-color: $light;
    border-radius: 7px;
}
.comments {
    height: 100px;
    border-radius: 7px;
    padding: 7px;
    border-style: solid;
    border-width: 1px;
}
.textStyle {
    text-align: center;

}

</style>
