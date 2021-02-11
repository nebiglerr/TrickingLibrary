<template>
  <div>
    <div v-if="item">
      {{ item.description }}
    </div>

    <v-row>
      <v-col cols="7">
        <comment-section :comments="comments" @send="sendComment" />
      </v-col>
      <v-col cols="5">
        <v-card>
          <v-card-title>
            Reviews({{ approveCount }}/5)
          </v-card-title>
          <v-card-text>
            <div v-if="reviews.length > 0">
              <div v-for="review in reviews" :key="review.id">
                <v-icon :color="reviewStatusColor(review.status)">
                  {{ reviewStatusIcon(review.status) }}
                </v-icon>
                Username<span v-if="review.comment">-- {{ review.comment }}</span>
              </div>
            </div>
            <div v-else>
              No Reviews
            </div>
            <v-divider class="my-3" />
            <v-text-field v-model="reviewComment" label="Review Comment" />
          </v-card-text>
          <v-card-actions class="justify-center">
            <v-btn
              v-for="action in reviewActions"
              :key="action.id"
              :color="reviewStatusColor(action.status)"
              :disabled="action.disable"
              @click="createReview(action.status)"
            >
              <v-icon>{{ reviewStatusIcon(action.status) }}</v-icon>
              {{ action.title }}
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>
<script>
import CommentSection from '@/components/comments/comment-section'

const endpointResolver = (type) => {
  if (type === 'trick') {
    return 'tricks'
  }
}
const REVIEW_STATUS = {
  APPROVED: 0,
  REJECTED: 1,
  WAITING: 2
}
const reviewStatusColor = (status) => {
  if (REVIEW_STATUS.APPROVED === status) {
    return 'green'
  }
  if (REVIEW_STATUS.REJECTED === status) {
    return 'red'
  }
  if (REVIEW_STATUS.WAITING === status) {
    return 'orange'
  }
  return ''
}
const reviewStatusIcon = (status) => {
  if (REVIEW_STATUS.APPROVED === status) {
    return 'mdi-check'
  }
  if (REVIEW_STATUS.REJECTED === status) {
    return 'mdi-close'
  }
  if (REVIEW_STATUS.WAITING === status) {
    return 'mdi-clock'
  }
  return ''
}
export default {
  component: { CommentSection },
  data: () => ({
    item: null,
    comments: [],
    reviews: [],
    reviewComment: '',
    replyId: 0
  }),
  computed: {
    reviewActions () {
      return [
        { title: 'Approve', status: REVIEW_STATUS.APPROVED, disable: false },
        { title: 'Reject', status: REVIEW_STATUS.REJECTED, disable: !this.reviewComment },
        { title: 'Wait', status: REVIEW_STATUS.WAITING, disable: !this.reviewComment }
      ]
    },
    approveCount () {
      return this.reviews.filter(x => x.status === REVIEW_STATUS.APPROVED).length
    }
  },

  created () {
    const { modId, type, trickId } = this.$route.params
    const endpoint = endpointResolver(type)
    this.$axios.$get('/api/' + endpoint + '/' + trickId).then((item) => {
      this.item = item
    })
    this.$axios.$get('/api/moderation-items/' + modId + '/comments')
      .then((comments) => {
        this.comments = comments
      })
    this.$axios.$get('/api/moderation-items/' + modId + '/reviews')
      .then((reviews) => {
        this.reviews = reviews
        console.log(this.reviews)
      })
  },
  methods: {
    sendComment (content) {
      const { modId } = this.$route.params
      return this.$axios.$post('/api/moderation-items/' + modId + '/comments',
        { content }).then((comment) => {
        this.comments.push(comment.valueOf())
        this.comment = ''
      })
    },
    createReview (status) {
      const { modId } = this.$route.params
      return this.$axios.$post('/api/moderation-items/' + modId + '/reviews',
        {
          comment: this.reviewComment,
          status
        }).then((review) => {
        this.reviews.push(review.valueOf())

        this.comment = ''
      })
    },
    reviewStatusColor,
    reviewStatusIcon

  }

}
</script>

<style scoped>

</style>
