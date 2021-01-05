<template>
  <div>
    <v-card-title>Create Difficulty</v-card-title>
    <v-card-text>
      <v-text-field v-model="form.name" label="Name" />
      <v-text-field v-model="form.description" label="Description" />
    </v-card-text>
    <v-card-actions>
      <v-btn @click="save">
        Save
      </v-btn>
    </v-card-actions>
  </div>
</template>

<script>

const initState = () => ({
  form: {
    name: '',
    description: ''

  }

})
export default {
  name: 'DifficultyForm',
  data: initState,
  methods: {
    async  save () {
      await this.$axios.post('api/difficulties', this.form)
    }
  }
}
</script>

<style scoped>

</style>
