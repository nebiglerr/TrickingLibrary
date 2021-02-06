<template>
  <item-content-layout>
    <template v-slot:content>
      <div v-if="submissions">
        <v-card v-for="s in submissions" :key="s.id" class="mb-3">
          <video-player :key="trick.id" :video="s.video" />
          <v-card-text>{{ s.description }}</v-card-text>
        </v-card>
      </div>
    </template>
    <template v-slot:item>
      <div class="text-h5">
        <span>   {{ trick.name }}</span>
        <v-chip class="mb-1 ml-2" small :to="/difficulty/+difficulty.id">
          {{ difficulty.name }}
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
    </template>
  </item-content-layout>
</template>

<script>
import { mapState, mapGetters } from 'vuex'
import VideoPlayer from '~/components/video-player'
import ItemContentLayout from '~/components/item-content-layout'

export default {
  component: { ItemContentLayout, VideoPlayer },
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
