<template>
  <div id="app">
    <TopHeader/>
    <Sidebar />
    <Footer />
    <div class="router-view">
      <router-view/>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
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
  padding: 25px;
  margin-left: 200px;
}
</style>
