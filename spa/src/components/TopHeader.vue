<template>
    <div id="rootDiv">
        <b-navbar type="dark" class="header">
            <b-button class="menu" variant="light" @click="openMenu()">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="#000000" class="bi bi-list" viewBox="0 0 16 16">
                    <path fill-rule="evenodd" d="M2.5 11.5A.5.5 0 0 1 3 11h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5zm0-4A.5.5 0 0 1 3 7h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5zm0-4A.5.5 0 0 1 3 3h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5z"/>
                </svg>
            </b-button>
            <b-navbar-brand class="brand" to="/"><div class="logo-wrap"><img src="../assets/logo-white.png"></div></b-navbar-brand>
            <b-form class="search-bar" @submit.prevent="submit">
                <b-form-input
                placeholder="Søg"
                v-model="searchBarInput"
                ></b-form-input>
            </b-form>
                    <b-dropdown offset="500" class="icon-only ml-auto">
                        <template #button-content>
                            <span class="ml-auto" v-if="User.firstName !== null && User.firstName !== undefined" style="margin: 0px; color: #fff;">{{ User.firstName + " " + User.lastName + " " }}</span>
                            <svg width="2.2em" height="2.2em" viewBox="0 0 16 16" class="bi bi-person-circle" fill="white" xmlns="http://www.w3.org/2000/svg">
                            <path d="M13.468 12.37C12.758 11.226 11.195 10 8 10s-4.757 1.225-5.468 2.37A6.987 6.987 0 0 0 8 15a6.987 6.987 0 0 0 5.468-2.63z"/>
                            <path fill-rule="evenodd" d="M8 9a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"/>
                            <path fill-rule="evenodd" d="M8 1a7 7 0 1 0 0 14A7 7 0 0 0 8 1zM0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8z"/>
                        </svg>
                        </template>
                        <template v-if="!loggedIn">
                            <b-dropdown-item to="/login">Log ind</b-dropdown-item>
                            <b-dropdown-item to="/register">Registrer</b-dropdown-item>
                        </template>
                        <template v-else>
                            <b-dropdown-item :to="'/profiles?user=' + id">Min profil</b-dropdown-item>
                            <b-dropdown-divider></b-dropdown-divider>
                            <b-dropdown-item to="/arbejdsopgave">Opret arbejdsopgave</b-dropdown-item>
                            <b-dropdown-item to="/mineTilmeldinger">Mine tilmeldinger</b-dropdown-item>
                            <b-dropdown-item to="/mytasks">Mine arbejdsopgaver</b-dropdown-item>
                            <b-dropdown-divider></b-dropdown-divider>
                            <b-dropdown-item @click="changeTheme">Skift til <template v-if="theme === 'light'">Mørkt tema</template><template v-else>Lyst tema</template></b-dropdown-item>
                            <b-dropdown-divider></b-dropdown-divider>
                            <b-dropdown-item @click="Logout">Log ud</b-dropdown-item>
                        </template>
                    </b-dropdown>
        </b-navbar>
    </div>
</template>
<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { GetLoggedInId, Logout, GetBrugerById } from "../api/user";
import { User } from "../types/user";
import { Dictionary } from "../types/dict";

@Component
export default class TopHeader extends Vue {
    loggedIn = false;
    searchBarInput = "";
    queries = {} as Dictionary<string>;
    id = "";
    theme = localStorage.getItem("theme");
    User = {} as User;

    openMenu() {
        document.getElementsByClassName("sidebar")[0].classList.toggle("open");
    }

    @Watch("$store.state.loggedIn") t() {
        this.loggedIn = this.$store.state.loggedIn;
        this.id = GetLoggedInId();

        GetBrugerById(Number(this.id)).then(response => {
            this.User = response.data;
        });
    }

    changeTheme() {
        if (this.theme === "light") localStorage.setItem("theme", "dark");
        else localStorage.setItem("theme", "light");

        this.theme = localStorage.getItem("theme");
        this.$store.commit("themeUpdate");
    }

    submit() {
        if (this.searchBarInput !== "") {
            this.NewQuery("search", this.searchBarInput);
        } else {
            this.DeleteFromQueryList("search");
            this.$store.commit("queriesState", this.queries);
        }
        this.ReplaceRoute(this.queries);
    }

    mounted() {
        if (GetLoggedInId() != null) {
            this.$store.commit("loggedInState", true);
        }
    }

    Logout() {
        this.$store.commit("loggedInState", false);
        if (this.$route.name !== "Home") this.$router.push({ name: "Home" });
        Logout();
    }

    Profile() {
        this.$router.push({ name: "Profile" });
    }

    NewQuery(name: string, value: string) {
        this.queries[name] = value;
        this.$store.commit("queriesState", this.queries);
    }

    DeleteFromQueryList(name: string) {
        delete this.queries[name];
    }

    ReplaceRoute(parameters: Dictionary<string>) {
        if (this.$route.name === "Overview") {
            this.$router.replace({ query: parameters });
        } else {
            this.$router.replace({ name: "Overview", query: parameters });
        }
    }
}
</script>

<style lang="scss" scoped>
@import "@/assets/main.scss";

.logo-wrap {
    height: 27px;

    img {
        display: block;
        max-width: 100%;
        max-height: 100%;
        margin: 0 auto;
    }
}

.menu svg {
    fill: #000!important;
}

.arbejdsopgave-btn {
    background: #fff;
    margin-right: 20px;
    color: #333;

    svg {
        margin-top: -2px;
        width: 20px;
        fill: #333;
        height: 20px;
        margin-right: 10px;
    }
}
.search-bar {
    width: 40%;
}
.brand {
    flex: 0 0 calc(210px - 1rem);
    margin: 0px;
    padding: 0px;
    margin-right: 1rem;
}
.header {
    position: fixed;
    height: 65px;
    left: 0;
    top: 0;
    width: 100%;
    z-index: 1020;
    background: $primary;
}
</style>
<style lang="scss">
.header .dropdown-menu.show {
    left: -100px;
}
.icon-only button {
    background: none!important;
    border: none!important;
    outline: none!important;
    box-shadow: none!important;
}
</style>
