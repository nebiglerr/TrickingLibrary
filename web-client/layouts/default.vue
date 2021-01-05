<template>
  <v-app dark>
    <v-app-bar app dark>
      <v-toolbar-title>Tircking Lirary</v-toolbar-title>

      <v-spacer />
      <content-creation-dialog />
    </v-app-bar>

    <v-main>
      <nuxt />
    </v-main>
  </v-app>
</template>

<script>
import ContentCreationDialog from '../components/content-creation/content-creation-dialog'
export default {
  component: {
    ContentCreationDialog
  }
}
</script>
