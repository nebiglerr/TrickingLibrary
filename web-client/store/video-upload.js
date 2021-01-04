import { UPLOAD_TYPE } from '~/data/enum'

export const strict = false
const initState = () => ({
  uploadPromise: null,
  active: false,
  type: '',
  step: 1
})

export const state = initState

export const mutations = {
  toggleActivity (state) {
    state.active = !state.active
    if (!state.active) {
      Object.assign(state, initState())
    }
  },
  setTask (state, uploadPromise) {
    state.uploadPromise = uploadPromise
    state.step++
  },
  incStep (state) {
    state.step++
  },
  setType (state, { type }) {
    state.type = type
    if (type === UPLOAD_TYPE.TRICK) {
      state.step++
    } else if (type === UPLOAD_TYPE.SUBMISSION) {
      state.step += 2
    }
  },
  resetVideos (state) {
    Object.assign(state, initState())
  }
}

export const actions = {

  startVideoUpload ({ commit, dispatch }, { formData }) {
    this.$axios.$post('/api/videos', formData, {
      headers: {
        Accept: 'text/html',
        'Content-Type': 'multipart/form-data'
      }
    }).then((element) => {
      console.log(element)

      commit('setTask', element)
    })
  },
  async createTricks ({ state, commit, dispatch }, { tricks, submission }) {
    if (state.type === UPLOAD_TYPE.TRICK) {
      console.log(tricks.name)
      const createdTrick = await this.$axios.$post('/api/tricks', tricks)
      console.log(createdTrick)
      submission.trickId = createdTrick.id
    }

    await this.$axios.$post('/api/submissions', submission)
  }
  /* async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize")
    await dispatch("library/loadContent")
  } */
}
