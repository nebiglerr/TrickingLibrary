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
    <div class="mx-2 ">
      <v-sheet class="pa-3 ma-2 sticky">
        <div class="text-h5">
          <span>   {{ trick.name }}</span>
          <v-chip class="mb-1 ml-2" small :to="/difficulty/+difficulty.id">
            {{ difficulty.name }} Difficulty
          </v-chip>
        </div>
        <v-divider class="my-1" />
        <div class="text-body-2">
          {{ trick.description }}
        </div>

        <v-divider class="my-1" />
        <div v-for="rd in relatedData" :key="rd.title">
          <div class="text-subtitle-1">
            {{ rd.title }}
          </div>
          <v-chip-group>
            <v-chip v-for="d in rd.data" :key="rd.idFactory(d)" small :to="rd.routeFactory(d)">
              {{ d.name }}
            </v-chip>
          </v-chip-group>
        </div>
      </v-sheet>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'

export default {
  async fetch () {
    const trickId = this.$route.params.trick
    this.trick = await this.trickById(this.$route.params.trick)
    this.difficulty = await this.difficultyById(this.trick.difficulty)
    await this.$store.dispatch('submissions/fetchSubmissionsForTricks', { trickId }, { root: true })
  },

  data: () => ({
    trick: [],
    difficulty: []
  }),
  computed: {
    ...mapState('submissions', ['submissions']),
    ...mapState('tricks', ['categories', 'tricks']),
    ...mapGetters('tricks', ['trickById', 'difficultyById']),

    relatedData () {
      return [
        {
          title: 'Categories',
          data: this.categories.filter(x => this.categories),
          idFactory: c => `category-${c.id}`,
          routeFactory: c => `/category/${c.id}`
        },
        {
          title: 'Prerequisites',
          data: this.tricks.filter(x => this.trick.prerequisites),
          idFactory: t => `trick-${t.id}`,
          routeFactory: t => `/trick/${t.id}`
        },
        {
          title: 'Progressions',
          data: this.tricks.filter(x => this.trick.progressions),
          idFactory: t => `trick-${t.id}`,
          routeFactory: t => `/trick/${t.id}`
        }
      ]
    }

  },
  head () {
    if (!this.tricks) { return {} }
    return {
      title: this.trick.name,
      meta: [
        { hid: 'description', name: 'description', content: this.trick.description }
      ]
    }
  }
}
</script>

<style scoped>

</style>
