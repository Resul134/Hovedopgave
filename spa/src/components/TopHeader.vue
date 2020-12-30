<template>
    <div id="rootDiv">
        <b-navbar type="dark" class="header">
            <b-navbar-brand class="brand" to="/"><div class="logo-wrap"><img src="../assets/logo-white.png"></div></b-navbar-brand>
            <b-form class="search-bar" @submit.prevent="submit">
                <b-form-input
                placeholder="Søg"
                v-model="searchBarInput"
                ></b-form-input>
            </b-form>
                    <b-button variant="light" class="ml-auto arbejdsopgave-btn" to="/arbejdsopgave" v-if="loggedIn">
                    <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-plus-circle" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                        <path fill-rule="evenodd" d="M8 15A7 7 0 1 0 8 1a7 7 0 0 0 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/>
                        <path fill-rule="evenodd" d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z"/>
                    </svg>
                    Opret arbejdsopgave
                    </b-button>
                    <b-dropdown :class="{ 'ml-auto' : !loggedIn }" offset="500" class="icon-only">
                        <template #button-content>
                            <svg width="2.2em" height="2.2em" viewBox="0 0 16 16" class="bi bi-person-circle" fill="white" xmlns="http://www.w3.org/2000/svg">
                            <path d="M13.468 12.37C12.758 11.226 11.195 10 8 10s-4.757 1.225-5.468 2.37A6.987 6.987 0 0 0 8 15a6.987 6.987 0 0 0 5.468-2.63z"/>
                            <path fill-rule="evenodd" d="M8 9a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"/>
                            <path fill-rule="evenodd" d="M8 1a7 7 0 1 0 0 14A7 7 0 0 0 8 1zM0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8z"/>
                        </svg>
                        </template>
                        <template v-if="!loggedIn">
                            <b-dropdown-item to="/login">Login</b-dropdown-item>
                            <b-dropdown-item to="/register">Register</b-dropdown-item>
                        </template>
                        <template v-else>
                            <b-dropdown-item to="/profile">Profile</b-dropdown-item>
                            <b-dropdown-item to="/mineTilmeldinger">Mine tilmeldinger</b-dropdown-item>
                            <b-dropdown-item to="/mytasks">Mine arbejdsopgaver</b-dropdown-item>
                            <b-dropdown-item @click="Logout">Logout</b-dropdown-item>
                        </template>
                    </b-dropdown>
        </b-navbar>
    </div>
</template>
<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { GetLoggedInId, Logout } from "../api/user";
import { Dictionary } from "../types/dict";

@Component
export default class TopHeader extends Vue {
    loggedIn = false;
    searchBarInput = "";
    queries = {} as Dictionary<string>;

    @Watch("$store.state.loggedIn") t() {
        this.loggedIn = this.$store.state.loggedIn;
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
        this.$router.push({ name: "Home" });
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
