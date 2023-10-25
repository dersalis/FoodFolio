<template>
  <v-card
    class="mx-auto my-12"
    max-width="368"
    >
    <v-card-item title="Florida">
      <template v-slot:subtitle> Extreme Weather Alert </template>
    </v-card-item>

    <div class="d-flex py-3 justify-space-between">
      <v-list-item density="compact" prepend-icon="mdi-calendar">
        <v-list-item-subtitle>23.10.2023</v-list-item-subtitle>
      </v-list-item>

      <v-list-item density="compact" prepend-icon="mdi-cash-multiple">
        <v-list-item-subtitle>20,00 zł</v-list-item-subtitle>
      </v-list-item>
    </div>
  </v-card>
</template>

<script lang="ts" setup>
import { Ref, ref } from 'vue';


</script>
