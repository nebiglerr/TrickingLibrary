<template>
  <v-app dark>
    <v-app-bar app dark>
      <v-toolbar-title>Tircking Lirary</v-toolbar-title>

      <v-spacer />
      <video-upload />
    </v-app-bar>

    <v-main>
      <nuxt />
    </v-main>
  </v-app>
</template>

<script>
import VideoUpload from '../components/video-upload'
export default {
  component: {
    VideoUpload
  }
}
</script>
