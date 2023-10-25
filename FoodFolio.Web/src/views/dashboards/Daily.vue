<template>
  <v-container class="fill-height">


    <v-responsive class="align-center text-center fill-height">



      <h1 class="text-h4 font-weight-bold" style="margin-bottom: 10px;">DZIEŃ</h1>
      <v-divider></v-divider>

      <v-row class="d-flex align-center justify-center">

        <v-col cols="auto">
          <dashboard-card></dashboard-card>
        </v-col>

        <v-col cols="auto">
          <dashboard-card></dashboard-card>
        </v-col>

        <v-col cols="auto">
          <dashboard-card></dashboard-card>
        </v-col>

      </v-row>
    </v-responsive>
  </v-container>
</template>

<script lang="ts" setup>
import { Ref, ref } from 'vue';
import DashboardCard from '../../components/dashboard/DashboardCard.vue'


</script>
