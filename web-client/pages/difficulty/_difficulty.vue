<template>
  <item-content-layout>
    <template v-slot:content>
      <trick-list :tricks="tricks" class="mx-2" />
    </template>
    <template v-slot:item>
      <div v-if="difficulty">
        <div class="text-h6">
          {{ difficulty.name }}
        </div>
        <v-divider class="my-1" />
        <div class="text-body-2">
          {{ difficulty.description }}
        </div>
      </div>
    </template>
  </item-content-layout>
</template>
<script>
import { mapGetters } from 'vuex'
import TrickList from '~/components/trick-list'
import ItemContentLayout from '~/components/item-content-layout'
export default {
  name: 'Difficulty',
  component: {
    TrickList,
    ItemContentLayout
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
