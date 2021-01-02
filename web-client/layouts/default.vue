<template>
  <v-app dark>
    <v-app-bar app dark>
      <v-toolbar-title>Tircking Lirary</v-toolbar-title>

      <v-spacer />

      <v-btn depressed @click="toggleActivity">
        <v-icon small>
          Upload
        </v-icon>
      </v-btn>
    </v-app-bar>
    <video-upload />
    <v-main>
      <nuxt />
    </v-main>
  </v-app>
</template>

<script>
import { mapMutations } from 'vuex'
import VideoUpload from '../components/video-upload'
export default {
  methods: mapMutations('video-upload', ['toggleActivity']),
  component: {
    VideoUpload
  }
}
</script>
