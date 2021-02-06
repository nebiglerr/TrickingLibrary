<template>
  <div class="video-container">
    <div class="play-button" :class="{'hide':playing}" @click="playing = !playing">
      <v-icon size="50">
        mdi-play
      </v-icon>
    </div>
    <video
      ref="video"
      muted
      loop
      :src="'http://localhost:5000/api/videos/'+video.videoLink"
      :poster="'http://localhost:5000/api/videos/'+video.thumbLink"
      preload="none"
    />
  </div>
</template>

<script>
//  <video ref="video" muted loop  controls />
export default {
  name: 'VideoPlayer',
  props: {
    video: {
      require: true,
      type: String
    }
  },
  data: () => ({
    playing: false
  }),
  watch: {
    playing (v) {
      if (v) {
        this.$refs.video.play()
      } else {
        this.$refs.video.pause()
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.video-container {
  position: relative;
  width: 100%;
  display: flex;

  .play-button {
    display: flex;
    justify-content: center;
    align-content: center;
    border-top-left-radius: inherit;
    border-top-right-radius: inherit;
    position: absolute;
    background-color: rgb(0, 0, 0, 0.36);
    width: 100%;
    height: 100%;
    z-index: 2;

    &.hide {
      opacity: 0;
    }
  }

  video {
    width: 100%;
    z-index: 1;
    border-top-left-radius: inherit;
    border-top-right-radius: inherit;
  }
}
</style>
