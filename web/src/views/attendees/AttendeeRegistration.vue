<template>
  <el-container class="AttendeeRegistration" v-loading="isLoading">
    <el-main class="AttendeeRegistration__main-content">
      <el-row type="flex" justify="center" class="AttendeeRegistration__row-container">
        <el-col :span="6">
          <el-card shadow="never">
            <img
              src="@/assets/logo.png"
              alt="logo"
              class="AttendeeRegistration__logo"
            >
            <p>
              Foursquare Gospel Church Venue Mgt
            </p>

            <div v-if="showNewPersonForm">
              <new-person-form @submit="handleNewPersonSubmit" />
            </div>

            <div v-if="showCheckPersonForm">
              <person-check-form @submit="handlePersonCheckSubmit" />
            </div>

            <div v-if="showSeatSelectionForm">
              <seat-selection-form @submit="handleSeatSelectionFormSubmit" />
            </div>

            <div v-if="showAssignedSeat">
              <el-descriptions
                title="Assigned Seat Information"
                direction="vertical"
                border
                :column="2"
              >
                <el-descriptions-item label="Venue Name">
                  {{assignedSeat.venueName}}
                </el-descriptions-item>

                <el-descriptions-item label="Seat Category">
                  <el-tag size="small">{{assignedSeat.category}}</el-tag>
                </el-descriptions-item>

                <el-descriptions-item label="Seat Number" :span="assignedSeat.associatedNumber ? 1 : 2">
                  <el-tag size="small">{{assignedSeat.number}}</el-tag>
                </el-descriptions-item>

                <el-descriptions-item label="Seat Number" v-if="assignedSeat.associatedNumber">
                  <el-tag size="small">{{assignedSeat.associatedNumber}}</el-tag>
                </el-descriptions-item>
              </el-descriptions>

              <el-button @click="handleClose" type="primary" class="AttendeeRegistration__close-btn">
                Close
              </el-button>
            </div>

            <div v-if="showNewPersonForm || showCheckPersonForm || showSeatSelectionForm">
              <el-button
                icon="el-icon-back"
                type="text"
                @click="goBackToList"
              >
                Go back
              </el-button>
            </div>

            <div
              class="AttendeeRegistration__person-list"
              v-if="!(showNewPersonForm || showCheckPersonForm || showSeatSelectionForm || showAssignedSeat)"
            >
              <p>Choose a users registered on the device</p>

              <div
                class="AttendeeRegistration__person-item"
                v-for="person in cachedUsers"
                :key="person.id"
              >
                {{ person.fullName }}
              </div>

              <div class="AttendeeRegistration__person-item">
                <el-button type="text" size="small" @click="showCheckPersonForm = true">
                  Registered User? Enter your phone number here.
                </el-button>
              </div>

              <div class="AttendeeRegistration__person-item">
                <el-button type="text" size="small" @click="showNewPersonForm = true">
                  Not Registered? Register here.
                </el-button>
              </div>
            </div>
          </el-card>
        </el-col>
      </el-row>
    </el-main>
  </el-container>
</template>

<script>
import api from '@/utils/api';
import NewPersonForm from '@/components/NewPersonForm.vue';
import PersonCheckForm from '@/components/PersonCheckForm.vue';
import SeatSelectionForm from '@/components/SeatSelectionForm.vue';

export default {
  name: 'AttendeeRegistration',
  components: {
    NewPersonForm,
    PersonCheckForm,
    SeatSelectionForm
  },
  props: {
    eventId: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      isLoading: true,
      cachedUsers: [],
      assignedSeat: null,
      selectedPerson: null,
      showAssignedSeat: false,
      showNewPersonForm: false,
      showCheckPersonForm: false,
      showSeatSelectionForm: false
    }
  },
  methods: {
    async checkPerson({ phoneNumber }) {
      try {
        const { data } = await api.get(`/v1/persons/check?phoneNumber=${phoneNumber}`);

         this.selectedPerson = data;
        this.cachePerson(data);
        this.showCheckPersonForm = false;
        this.showSeatSelectionForm = true;
      } catch (error) {
        const { data, status } = error.response;

        console.log(status, data.message);
      }
    },
    async registerNewPerson(body) {
      try {
        const { data } = await api.post('/v1/persons', body);

        this.selectedPerson = data;
        this.cachePerson(data);
        this.showNewPersonForm = false;
        this.showSeatSelectionForm = true;
      } catch (error) {
        const { data, status } = error.response;

        console.log(status, data.message);
      }
    },
    async attenndeeCheckin(body) {
      try {
        const { data } = await api.post(`/v1/events/${this.eventId}/checkin`, body);

        console.log(data, 'data');
        this.selectedPerson = null;
        this.showSeatSelectionForm = false;
        this.assignedSeat = data;
        this.showAssignedSeat = true;
      } catch (error) {
        const { data, status } = error.response;

        console.log(status, data.message);
      }
    },
    handleShowRegisteredUser() {},
    handleNewPersonSubmit(body) {
      this.registerNewPerson(body);
    },
    handlePersonCheckSubmit(body) {
      this.checkPerson(body);
    },
    handleSeatSelectionFormSubmit(body) {
      const payload = {
        ...body,
        personId: this.selectedPerson.id
      };

      this.attenndeeCheckin(payload);
    },
    cachePerson(person) {
      const found = this.cachedUsers.find(user => user.id === person.id);

      if (!found) {
        this.cachedUsers.push(person);
      }

      localStorage.setItem('cachedusers', JSON.stringify(this.cachedUsers));
    },
    goBackToList() {
      this.showNewPersonForm = false;
      this.showCheckPersonForm = false;
      this.showSeatSelectionForm = false;

      this.selectedPerson = null;
    },
    handleClose() {}
  },
  mounted() {
    const cachedUsers = localStorage.getItem('cachedusers');

    if (cachedUsers) {
      try {
        this.cachedUsers = JSON.parse(cachedUsers);
      } catch (error) {
        console.log(error);
      }
    }

    this.isLoading = false;
  },
  beforeDestroy() {
    this.selectedPerson = null;
    this.assignedSeat
  }
}
</script>

<style lang="scss">
.AttendeeRegistration {
  height: 100vh;

  &__row-container {
    margin-top: 100px;
  }

  &__logo {
    height: 70px;
    width: 70px;
  }

  &__person-item {
    padding: 5px 0;

    &:not(:last-of-type) {
      border-bottom: 1px solid #dcdfe6;
    }
  }

  &__close-btn {
    margin-top: 15px;
  }
}
</style>