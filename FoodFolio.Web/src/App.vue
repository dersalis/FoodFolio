<template>


  <router-view />

  <v-app>
    <v-container>
      <v-bottom-navigation
        v-model="tabNo"
      >
        <v-btn value="recent">
          <v-icon>mdi-calendar</v-icon>
          <span>Dzisiaj</span>
        </v-btn>

        <v-btn value="favorites">
          <v-icon>mdi-calendar-month</v-icon>
          <span>Tydzień</span>
        </v-btn>
      </v-bottom-navigation>
    </v-container>
  </v-app>
</template>

<script lang="ts" setup>
import { Ref, ref } from 'vue';

  let tabNo: Ref<number> = ref(0);
</script>
