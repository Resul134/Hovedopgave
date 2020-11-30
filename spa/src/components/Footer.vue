<template>
    <footer class="site-footer">
      <div class="container">
        <div class="row 12">
          <div class="col-sm-12 col-md-6 col-xl-6 rowMargin">
            <h6>{{InfoTitel}}</h6>
            <p class="text-justify">{{InfoText}}</p>
          </div>
          <div class="col-xs-6 col-md-6 col-xl-6 textRight">
            <h6 class="">{{KontaktTitel}}</h6>
            <ul class="footer-links">
              <li><a href="mailto:support@workio.dk" target="_blank">{{MailText}}</a></li>
            </ul>
          </div>
        </div>
        <hr>
      </div>
      <div class="container">
        <div class="row justify-content-center">
            <p class="copyright-text">{{Copyright}}</p>
          </div>
        </div>
</footer>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class Footer extends Vue {
    InfoTitel = "Om Os";
    InfoText = "Vi bestræber os på at være den førende markedsplads indenfor tjenesteydelser";
    KontaktTitel = "Kontakt Os";
    MailText = "Kundeservice";
    Copyright = "Copyright © 2017 All Rights Reserved by Workio";
}
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";
.site-footer
{
  background-color: $primary;
  padding:25px 0 20px;
  font-size:15px;
  line-height:24px;
}
.site-footer .container {
max-width: 1980px;
}
.site-footer hr
{
  border-top-color: $light;
}
.site-footer p {
    color: $light;
}
.site-footer h6
{
  color: $light;
  font-size:16px;
  text-transform:uppercase;
  margin-top:5px;
  letter-spacing:2px
}
.site-footer a
{
  color: $light;
}
.site-footer a:hover
{
  color:#3366cc;
  text-decoration:none;
}
.footer-links
{
  padding-left:0;
  list-style:none
}
.footer-links a:active,.footer-links a:focus,.footer-links a:hover
{
  color:#3366cc;
  text-decoration:none;
}
.copyright-text
{
  margin:0
}
@media only screen and (min-width: 768px){
    .textRight{
    text-align: -webkit-right;
    }
}
</style>
