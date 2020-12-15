<template>
<div>
    <b-row>
        <b-col cols="1"></b-col>
        <b-col cols="7">
            <div class="profile">
                <b-row>
                    <b-col cols="3">
                        <h1>{{ userUsername }}</h1>
                    </b-col>
                    <b-col cols="6"></b-col>
                    <b-col cols="3">
                        <svg xmlns="http://www.w3.org/2000/svg" width="7em" height="7em " fill="currentColor" class="bi bi-person" viewBox="0 0 16 16">
                            <path fill-rule="evenodd" d="M10 5a2 2 0 1 1-4 0 2 2 0 0 1 4 0zM8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm6 5c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4zm-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664h10z"/>
                        </svg>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col>
                        <p class="divider">Navn:<span>{{ userFirstName }} {{ userLastName }}</span></p>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col cols="8">
                        <p class="divider">Email:<span>{{ userEmail }}</span></p>
                    </b-col>
                    <b-col cols="4">
                        <p class="divider">Telefon:<span>{{ userPhone }}</span></p>
                    </b-col>
                </b-row>
                <h5 style="font-weight: bold; padding-bottom: 10px;">Beskrivelse</h5>
                <b-row>
                    <b-col class="desc">
                        {{ userDescription }}
                    </b-col>
                </b-row>
            </div>
        </b-col>
        <b-col cols="3">
            <div class="profile">
                <p class="font-weight-bold text-center">Kompetencer</p>
                <div class="list">
                    <p v-for="(qualification, idx) in userQualifications" :key="idx">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-arrow-right-short" viewBox="0 0 16 16">
                            <path fill-rule="evenodd" d="M4 8a.5.5 0 0 1 .5-.5h5.793L8.146 5.354a.5.5 0 1 1 .708-.708l3 3a.5.5 0 0 1 0 .708l-3 3a.5.5 0 0 1-.708-.708L10.293 8.5H4.5A.5.5 0 0 1 4 8z"/>
                        </svg>
                        {{ qualification.skill }}
                        </p>
                </div>
            </div>
        </b-col>
        <b-col cols="1"></b-col>
    </b-row>
</div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { GetBrugerById } from "../api/user";
import { GetQualificationsByUserId } from "../api/qualification";
import { User } from "../types/user";
import { Qualification } from "../types/qualification";

@Component
export default class Profiles extends Vue {
    user = {} as User;
    userUsername = "";
    userPassword = "";
    userFirstName = "";
    userLastName = "";
    userBirthday: Date = new Date();
    userGender = "";
    userPhone = "";
    userEmail = "";
    userDescription = "";
    userQualifications = Array<Qualification>();

    @Watch("$route.query", { immediate: true, deep: true })
    OnParamChange() {
        this.Setup();
    }

    Setup() {
        let id = 0;
        if (this.$route.query.user) {
            id = Number(this.$route.query.user);
        } else {
            console.log("Should be sent to 404 page");
        }
        GetBrugerById(id).then(response => {
            this.user = response.data;
            this.userUsername = response.data.username;
            this.userFirstName = response.data.firstName;
            this.userLastName = response.data.lastName;
            this.userGender = response.data.gender;
            this.userPhone = response.data.phone;
            this.userEmail = response.data.email;
            this.userBirthday = response.data.birthday;
            this.userDescription = response.data.description;
        });
        GetQualificationsByUserId(id).then(response => {
            this.userQualifications = response.data;
        });
    }

    mounted() {
        this.Setup();
    }
}
</script>

<style lang="scss" scoped>
@import "@/assets/main.scss";

.profile{
    background: $light;
    padding: 40px;
    border-radius: 12px;
    p {
        overflow-wrap: break-word;
    }
}

.desc {
    overflow-wrap: break-word;
}

.font-weight-bold {
    border-bottom: 1px solid darken($light, 10);
    padding-bottom: 10px;
    margin-bottom: 20px;

}
p.divider {
    border-bottom: 1px solid darken($light, 10);
    padding-bottom: 10px;
    margin-bottom: 20px;
    span {
        font-weight: bold;
        margin-left: 10px;
    }
}
h1 {
    margin-bottom: 35px;
    font-weight: bold;
}
</style>
