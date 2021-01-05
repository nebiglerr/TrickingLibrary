<template>
  <div class="d-flex justify-center align-start">
    <div v-if="submissions" class="mx-2">
      <div v-for="x in 1" :key="x.id">
        <div v-for="s in submissions" :key="s.id">
          {{ s.id }}-{{ s.description }}-{{ s.trickId }}
          <div>
            <video width="400" controls :src="'http://localhost:5000/api/videos/'+s.video" />
          </div>
        </div>
      </div>
    </div>
    <div class="mx-2 sticky">
      Tricks : {{ $route.params.trick }}
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  async fetch () {
    const trickId = this.$route.params.trick

    await this.$store.dispatch('submissions/fetchSubmissionsForTricks', { trickId }, { root: true })
  },
  computed: mapState('submissions', ['submissions'])

}
</script>

<style scoped>
.sticky{
position: -webkit-sticky;
  position: sticky;
  top: 62px;
}
</style>
