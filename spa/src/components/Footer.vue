<template>
    <footer class="site-footer">
      <div class="container-fluid">
        <div class="row">
          <div class="col-xs-6 col-md-6 col-xl-6" style="padding: 0;">
            <h6>{{InfoTitel}}</h6>
            <p class="text-justify">{{InfoText}}</p>
          </div>
          <div class="col-xs-6 col-md-6 col-xl-6 textRight" style="padding: 0">
            <h6 class="">{{KontaktTitel}}</h6>
            <a href="mailto:support@workio.dk" target="_blank">{{MailText}}</a>
          </div>
        </div>
      </div>
      <p class="copyright-text">{{Copyright}}</p>
</footer>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class Footer extends Vue {
    InfoTitel = "Om os";
    InfoText = "Vi bestræber os på at være den førende markedsplads indenfor tjenesteydelser";
    KontaktTitel = "Kontakt os";
    MailText = "Kundeservice";
    Copyright = "Copyright © 2017 All Rights Reserved by Workio";
}
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";
.site-footer {
    margin-top: 40px;
    border-top: 1px solid $light;
    width: calc(100% - #{$sidebar-width});
    margin-left: $sidebar-width;
    padding:25px;
    font-size:15px;
    line-height:24px;
    background: #fff;
    color: #212121;
}

.copyright-text {
    margin-top: 10px;
    color: darken($light, 50)
}

.textRight {
    text-align: right;
}

.site-footer h6 {
    font-size: 24px;
    font-weight: bold;
}
</style>
