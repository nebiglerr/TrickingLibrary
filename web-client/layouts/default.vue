<template>
  <v-app dark>
    <v-app-bar app dark>
      <nuxt-link class="text-h5  text--primary" style="text-decoration: none" to="/">
        Tircking Lirary
      </nuxt-link>

      <v-spacer />
      <content-creation-dialog />
    </v-app-bar>

    <v-main>
      <v-container>
        <nuxt />
      </v-container>
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
