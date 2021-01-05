<template>
  <div id="app" :class="theme">
    <TopHeader/>
    <Sidebar />
    <div class="router-view">
      <router-view/>
    </div>
    <Footer />
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import TopHeader from "@/components/TopHeader.vue";
import Sidebar from "@/components/Sidebar.vue";
import Footer from "@/components/Footer.vue";
@Component({
    components: {
        TopHeader,
        Sidebar,
        Footer
    }
})
export default class App extends Vue {
    theme = localStorage.getItem("theme");

    mounted() {
        if (localStorage.getItem("theme") === undefined || localStorage.getItem("theme") === null) {
            localStorage.setItem("theme", "light");
            this.theme = localStorage.getItem("theme");
        }
    }

    @Watch("$store.state.themeUpdate") t() {
        this.theme = localStorage.getItem("theme");
    }
}

</script>

<style lang="scss">
@import "./assets/main.scss";

*, *:before, *:after {
  box-sizing: border-box;
}

h1,h2,h3,h4,h5,h6,p,span,div {
  color: $gray;
}

a {
  color: $primary;
}

.router-view {
  margin-top: 65px;
  padding: 25px;
  margin-left: $sidebar-width;
}
</style>
