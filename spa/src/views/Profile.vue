<template>
  <div>
      <h1>Profil</h1>
      <div id="userInfo" class="user-info">
          <b-row>
              <b-col>
                  <div class="profile">
                      <h4 style="text-align:center;">Profil information</h4>
                    <div class="content-flex">
                      <b>Username: </b>
                      <b-form-input :value="user.username" disabled v-if="!edit"></b-form-input>
                      <b-form-input v-else v-model="user_username"></b-form-input>
                  </div>
                  <div class="content-flex">
                      <b>Password: </b>
                      <b-form-input :type="type" :value="user.password" disabled v-if="!edit"></b-form-input>
                      <b-form-input v-else v-model="user_password"></b-form-input>
                  </div>
                    <div class="content-flex">
                      <b>Fornavn:</b>
                          <b-form-input :value="user.firstName" disabled v-if="!edit"></b-form-input>
                          <b-form-input v-else v-model="user_firstName"></b-form-input>
                  </div>
                  <div class="content-flex">
                      <b>Efternavn:</b>
                          <b-form-input :value="user.lastName" disabled v-if="!edit"></b-form-input>
                          <b-form-input v-else v-model="user_lastName"></b-form-input>
                  </div>
                  <div class="content-flex">
                      <b>Fødselsdato:</b>
                      <p>{{ user.birthday | formatDate }}</p>
                  </div>
                  <div class="content-flex">
                      <b>Køn: </b>
                      <p>{{ user.gender }}</p>
                  </div>
                  <div class="content-flex">
                      <b>Telefon: </b>
                      <b-form-input :value="user.phone" disabled v-if="!edit"></b-form-input>
                      <b-form-input v-else v-model="user_phone"></b-form-input>
                  </div>
                  <div class="content-flex">
                      <b>Email: </b>
                      <b-form-input :value="user.email" disabled v-if="!edit"></b-form-input>
                      <b-form-input v-else v-model="user_email"></b-form-input>
                  </div>
                  <b-button  @click="redigerProfil">Rediger</b-button>
                  </div>
              </b-col>
              <b-col></b-col>
          </b-row>
      </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { GetLoggedInId, GetBrugerById } from "../api/user";
import { User } from "../types/user";

@Component
export default class Home extends Vue {
    user = {} as User;
    birthDay = "";
    edit = false;
    user_username = "";
    user_password = "";
    user_firstName = "";
    user_lastName = "";
    user_birthday = "";
    user_gender = "";
    user_phone = "";
    user_email = "";
    // LogOut() {
    //     Logout();
    //     this.$router.push({ name: "Login" });
    // }

    redigerProfil() {
        this.edit = !this.edit;
    }

    deleteProfil() {
        // måske en "Are you sure" box her
        DeleteBrugerById(GetLoggedInId()).then(response => {
            console.log(response.data);
            // feedback for user here, eventuelt en toast
        }).catch(() => {
            console.log("Couldn't get user ID");
            //feedback if it went wrong here
        })
    }

    mounted() {
        GetBrugerById(GetLoggedInId()).then(response => {
            this.user = response.data;
        });
    }
}
</script>

<style lang="scss" scoped>
@import "@/assets/main.scss";

.content-flex{
    display: flex;
    justify-content: space-between;
    border-radius: 5px;
    border-bottom: 1px solid black;
    margin-top: 5px;
}

.input-flex{
    display: flex;
    justify-content: flex-end;
}
input{
    width: 50%;
}

.profile{
    border-radius: 7px;
    padding: 10px;
    background: $light;
    height: 400px;
}

</style>
