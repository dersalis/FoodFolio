<template>
  <v-container class="fill-height">


    <v-responsive class="align-center text-center fill-height">



      <h1 class="text-h4 font-weight-bold" style="margin-bottom: 10px;">TYDZIEŃ</h1>
      <v-divider></v-divider>

      <v-row class="align-center">
        <v-col cols="auto" class="date-col">
          <div class="text-h5 font-weight-light">Poniedziałek</div>
          <div class="text-body-3 font-weight-light date-text">23.10.2023</div>
        </v-col>
        <v-col cols="auto">
          <dashboard-item></dashboard-item>
        </v-col>
        <v-col cols="auto">
          <dashboard-item></dashboard-item>
        </v-col>
        <v-col cols="auto">
          <dashboard-item></dashboard-item>
        </v-col>
      </v-row>

      <v-row class="align-center">
        <v-col cols="auto" class="date-col">
          <div class="text-h5 font-weight-light">Poniedziałek</div>
          <div class="text-body-3 font-weight-light date-text">23.10.2023</div>
        </v-col>
        <v-col cols="auto">
          <dashboard-item></dashboard-item>
        </v-col>
        <v-col cols="auto">
          <dashboard-item></dashboard-item>
        </v-col>
        <v-col cols="auto">
          <dashboard-item></dashboard-item>
        </v-col>
      </v-row>

      <v-row class="align-center">
        <v-col cols="auto" class="date-col">
          <div class="text-h5 font-weight-light">Poniedziałek</div>
          <div class="text-body-3 font-weight-light date-text">23.10.2023</div>
        </v-col>
        <v-col cols="auto">
          <dashboard-item></dashboard-item>
        </v-col>
        <v-col cols="auto">
          <dashboard-item></dashboard-item>
        </v-col>
        <v-col cols="auto">
          <dashboard-item></dashboard-item>
        </v-col>
      </v-row>

    </v-responsive>
  </v-container>
</template>

<script lang="ts" setup>
import { Ref, ref } from 'vue';
import DashboardItem from '../../components/dashboard/DashboardItem'


</script>

<style>
  .date-col {
    margin-right: 40px;
  }
  .date-text {
    text-align: left;
  }
</style>
