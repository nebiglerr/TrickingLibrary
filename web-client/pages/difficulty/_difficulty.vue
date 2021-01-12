<template>
  <div class="d-flex mt-3   justify-center align-start">
    <trick-list :tricks="tricks" class="mx-2" />

    <div class="mx-2 ">
      <v-sheet v-if="difficulty" class="pa-3 ma-2 sticky">
        <div class="text-h6">
          {{ difficulty.name }}
        </div>
        <v-divider class="my-1" />
        <div class="text-body-2">
          {{ difficulty.description }}
        </div>
      </v-sheet>
    </div>
  </div>
</template>
<script>
import { mapGetters } from 'vuex'
import TrickList from '~/components/trick-list'
export default {
  name: 'Difficulty',
  component: {
    TrickList
  },
  data: () => ({
    difficulty: null,
    tricks: []

  }),

  computed: {
    ...mapGetters('tricks', ['difficultyById'])

  },
  async fetch () {
    const difficultyId = this.$route.params.difficulty
    this.difficulty = await this.difficultyById(difficultyId)
    this.tricks = await this.$axios.$get('/api/difficulties/' + difficultyId + '/tricks')
    console.log(this.tricks)
  },
  head () {
    if (!this.difficulty) {
      return {}
    }
    return {
      title: this.difficulty.name,
      meta: [
        { hid: 'description', name: 'description', content: this.difficulty.description }
      ]
    }
  }
}
</script>

<style scoped>

</style>
